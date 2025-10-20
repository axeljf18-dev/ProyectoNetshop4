using Microsoft.Data.SqlClient;
using ProyectoNetshop.Cruds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoNetshop.formularios
{
    public partial class usuarios : Form
    {
        // Campo que guarda el ID del usuario seleccionado en la grilla
        // Inicializado en -1 para indicar que ningun usuario se eligio
        private int _usuarioSeleccionadoId = -1;

        public usuarios()
        {
            InitializeComponent();

            //Al cargar la vista, ejecuta usuarios_Load
            Load += usuarios_Load;

            // Al hacer clic en cualquier celda de la grilla llamamos al método que carga los datos de esa fila
            dgvUsuarios.CellClick += DgvUsuarios_CellClick;

            // Manejar clics en zonas vacías del grid
            dgvUsuarios.MouseDown += DgvUsuarios_MouseDown;

            // Si se hace clic en el formulario fuera del grid, limpiar selección
            this.Click += Form_Or_Container_Click;

            // Cada vez que se cambie el texto de búsqueda por DNI se aplica el filtro a la grilla
            tbBusquedaDniUsuario.TextChanged += Busqueda_TextChanged;
            tbBusquedaNombreUsuario.TextChanged += Busqueda_TextChanged;

            // Enmascarar por defecto
            tbContraseniaUsuario.UseSystemPasswordChar = true;

            // Mostrar el CheckBox todo el tiempo
            cbOcultalContraseniaUser.Visible = true;
            cbOcultalContraseniaUser.Checked = false;
            cbOcultalContraseniaUser.CheckedChanged += cbOcultalContraseniaUser_CheckedChanged;

            dgvUsuarios.CellFormatting += DgvUsuarios_CellFormatting;

            // Configuro largo máximo
            tbDniUsuario.MaxLength = 8;
            tbTelefonoUsuario.MaxLength = 10;

            // Asigno el mismo manejador a ambos TextBox
            tbDniUsuario.KeyPress += TextBox_OnlyDigits_KeyPress;
            tbTelefonoUsuario.KeyPress += TextBox_OnlyDigits_KeyPress;

            // Solo letras (y espacios) para el nombre
            tbBusquedaNombreUsuario.KeyPress += TextBox_OnlyLetters_KeyPress;
            // Solo dígitos para el DNI
            tbBusquedaDniUsuario.KeyPress += TextBox_OnlyDigits_KeyPress;
        }

        private void usuarios_Load(object sender, EventArgs e)
        {
            // Mostramos el formato corto de fecha
            fechaNacimientoUsuario.Format = DateTimePickerFormat.Short;

            // Habilitamos el checkbox para activar/desactivar la fecha
            fechaNacimientoUsuario.ShowCheckBox = true;

            // Por defecto, que venga “sin fecha” (unchecked)
            fechaNacimientoUsuario.Checked = false;

            // Por defecto muestro ambos
            cbActivosUsuarios.Checked = true;
            cbInactivosUsuarios.Checked = true;

            cbActivosUsuarios.CheckedChanged += Filtro_CheckedChanged;
            cbInactivosUsuarios.CheckedChanged += Filtro_CheckedChanged;

            // Por defecto, activo = 1
            rbActivoUsuarioSi.Checked = true;

            rbMasculinoUsuario.Checked = false;
            rbFemeninoUsuario.Checked = false;
            rbOtrosUsuario.Checked = true;

            FiltrarYRefrescar();

            // Obtener la lista de perfiles
            var perfiles = ObtenerPerfiles();

            cbPerfilUsuario.DataSource = perfiles;
            cbPerfilUsuario.DisplayMember = "descripcion";
            cbPerfilUsuario.ValueMember = "id_perfil";

            // Selecciono “Vendedor” por defecto
            var vendedor = perfiles
                .FirstOrDefault(p => p.descripcion.Equals("Vendedor", StringComparison.OrdinalIgnoreCase));

            if (vendedor != null)
                cbPerfilUsuario.SelectedValue = vendedor.id_perfil;

            // Obtengo la lista de usuarios
            var usuarios = ObtenerUsuarios();

            // Obtener la descripcion de perfiles de cada usuario
            for (int i = 0; i < usuarios.Count; i++)
            {
                for (int j = 0; j < perfiles.Count; j++)
                {
                    if (usuarios[i].id_perfil == perfiles[j].id_perfil)
                    {
                        usuarios[i].descripcion = perfiles[j].descripcion;
                        break;
                    }
                }
            }

            // Configuramos el DataGridView manualmente
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id_usuario",
                DataPropertyName = "id_usuario",
                HeaderText = "ID Usuario"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nombre",
                DataPropertyName = "nombre",
                HeaderText = "Nombre"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "apellido",
                DataPropertyName = "apellido",
                HeaderText = "Apellido"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "email",
                DataPropertyName = "email",
                HeaderText = "Email"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "dni",
                DataPropertyName = "dni",
                HeaderText = "DNI"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "telefono",
                DataPropertyName = "telefono",
                HeaderText = "Telefono"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "sexo",
                DataPropertyName = "sexo",
                HeaderText = "Sexo"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fecha_nacimiento",
                DataPropertyName = "fecha_nacimiento",
                HeaderText = "Fecha de Nacimiento"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "pass",
                DataPropertyName = "pass",
                HeaderText = "Contraseña"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id_perfil",
                DataPropertyName = "descripcion",
                HeaderText = "Perfil"
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colActivo",
                HeaderText = "Activo",
                DataPropertyName = "ActivoTexto"
            });
            
            // Asigno la lista ya procesada
            dgvUsuarios.DataSource = usuarios;
        }

        private void DgvUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Si es la columna “pass”
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "pass")
            {
                // Asigna siempre 10 asteriscos
                e.Value = "***************";
                e.FormattingApplied = true;
            }
        }

        // Nuevo manejador: clic del mouse sobre el DataGridView
        private void DgvUsuarios_MouseDown(object? sender, MouseEventArgs e)
        {
            var hit = dgvUsuarios.HitTest(e.X, e.Y);

            // Si el clic no fue sobre una fila válida (hit.RowIndex < 0) limpiamos selección
            if (hit.RowIndex < 0)
            {
                dgvUsuarios.ClearSelection();
                _usuarioSeleccionadoId = -1;
                LimpiarControles();
            }
            else
            {
                // Si clic sobre una fila, seleccionamos esa fila (comportamiento visual)
                dgvUsuarios.ClearSelection();
                dgvUsuarios.Rows[hit.RowIndex].Selected = true;

                // También podemos forzar que se dispare el mismo flujo que CellClick
                // para cargar controles si no se usa CellClick por Key/Mouse:
                // var args = new DataGridViewCellEventArgs(hit.ColumnIndex, hit.RowIndex);
                // DgvUsuarios_CellClick(this, args);
            }
        }

        // Manejar clics fuera del DataGridView — ejemplo en el formulario o contenedores principales
        private void Form_Or_Container_Click(object? sender, EventArgs e)
        {
            dgvUsuarios.ClearSelection();
            _usuarioSeleccionadoId = -1;
            LimpiarControles();
        }

        private void tbNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite solo letras y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void tbApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite solo letras y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            // Se confirmar cual acción de se va a ejecutar (crear o actualizar)
            string accion = _usuarioSeleccionadoId < 0 ? "crear" : "actualizar";
            var dr = MessageBox.Show($"¿Seguro que deseas {accion} este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            // Si estamos en modo actualizar, obtenemos el usuario actualmente mostrado en la grilla
            Usuario_model usuarioSeleccionado = null;
            if (_usuarioSeleccionadoId >= 0 && dgvUsuarios.CurrentRow != null)
                usuarioSeleccionado = dgvUsuarios.CurrentRow.DataBoundItem as Usuario_model;


            // Se armar el objeto Usuario_model
            var usuario = new Usuario_model
            {
                nombre = tbNombreUsuario.Text.Trim(),
                apellido = tbApellidoUsuario.Text.Trim(),
                email = tbEmailUsuario.Text.Trim(),
                pass = Encoding.UTF8.GetBytes(tbContraseniaUsuario.Text),
                activo = rbActivoUsuarioSi.Checked ? 1 : 0,
                sexo = rbMasculinoUsuario.Checked ? "Masculino"
                                  : rbFemeninoUsuario.Checked ? "Femenino"
                                                                  : "Otros",
                //fecha_nacimiento = fechaNacimientoUsuario.Checked ? fechaNacimientoUsuario.Value.Date : (DateTime?)null,
                // Si el DateTimePicker está chequeado uso su valor, si no estoy en actualización uso null,
                // si estoy actualizando y no lo tocaron, conservo el valor del usuario seleccionado.
                fecha_nacimiento = fechaNacimientoUsuario.Checked ? fechaNacimientoUsuario.Value.Date
                                                                  : (_usuarioSeleccionadoId >= 0 ? usuarioSeleccionado?.fecha_nacimiento : (DateTime?)null),
                telefono = long.TryParse(tbTelefonoUsuario.Text.Trim(), out long tel) ? (long?)tel : (long?)null,
                dni = int.Parse(tbDniUsuario.Text),
                id_perfil = Convert.ToInt32(cbPerfilUsuario.SelectedValue)
            };

            // Se ejecuta el INSERT o UPDATE
            int filas;
            if (_usuarioSeleccionadoId < 0)
                filas = Usuario_controller.agregarUsuario(usuario);
            else
            {
                usuario.id_usuario = _usuarioSeleccionadoId;
                filas = Usuario_controller.actualizarUsuario(usuario);
            }

            if (filas == 1)
                MessageBox.Show(_usuarioSeleccionadoId < 0 ? "Usuario creado con éxito." : "Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Ocurrió un error durante la operación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            FiltrarYRefrescar();
            LimpiarControles();
        }

        private void cbPerfilUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usuario_model usuario = new Usuario_model();
            if (cbPerfilUsuario.SelectedItem is Perfil_model perfil)
            {
                int idSeleccionado = perfil.id_perfil;
                string descSeleccion = perfil.descripcion;
                usuario.id_perfil = idSeleccionado;
            }
        }

        private List<Perfil_model> ObtenerPerfiles()
        {
            var lista = new List<Perfil_model>();

            using (SqlConnection conexion = BD.BaseDeDatos.obtenerConexion())
            using (SqlCommand comando = new SqlCommand("SELECT id_perfil, descripcion, activo FROM perfil", conexion))
            {
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var perfil = new Perfil_model(
                            reader.GetInt32(0), // id_perfil
                            reader.GetString(1), // descripcion
                            reader.GetInt32(2) // activo
                        );
                        lista.Add(perfil);
                    }
                }
            }

            return lista;
        }

        private List<Usuario_model> ObtenerUsuarios()
        {
            var lista = new List<Usuario_model>();
            using var con = BD.BaseDeDatos.obtenerConexion();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT id_usuario, nombre, apellido, email, pass, activo, sexo, fecha_nacimiento, telefono, dni, id_perfil FROM usuario";
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                // fecha_nacimiento ya la tienes bien:
                DateTime? fn = rd.IsDBNull(7)
                    ? (DateTime?)null
                    : rd.GetDateTime(7);
                //long? tel = rd.IsDBNull(8) ? (long?)null : rd.GetInt64(8);
                // telefono: manejar bigint o varchar en la DB
                long? tel = null;
                if (!rd.IsDBNull(8))
                {
                    var fieldType = rd.GetFieldType(8);
                    if (fieldType == typeof(long) || fieldType == typeof(Int64))
                    {
                        tel = rd.GetInt64(8);
                    }
                    else
                    {
                        // intentar leer como string y parsear de forma segura
                        string telStr = rd.GetString(8);
                        if (long.TryParse(telStr, out long parsed))
                            tel = parsed;
                        else
                            tel = null; // valor por defecto si no es numérico
                    }
                }

                var usuario = new Usuario_model
                {
                    id_usuario = rd.GetInt32(0),
                    nombre = rd.GetString(1),
                    apellido = rd.GetString(2),
                    email = rd.GetString(3),
                    pass = (byte[])rd["pass"],
                    activo = rd.GetInt32(5),
                    sexo = rd.GetString(6),
                    fecha_nacimiento = fn,
                    telefono = tel,
                    dni = rd.GetInt32(9),
                    id_perfil = rd.GetInt32(10)
                };

                lista.Add(usuario);
            }

            return lista;
        }
        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var u = (Usuario_model)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;
            _usuarioSeleccionadoId = u.id_usuario;

            tbNombreUsuario.Text = u.nombre;
            tbApellidoUsuario.Text = u.apellido;
            tbEmailUsuario.Text = u.email;
            tbContraseniaUsuario.Text = u.pass != null ? Encoding.UTF8.GetString(u.pass) : string.Empty;
            tbContraseniaUsuario.UseSystemPasswordChar = true;
            cbOcultalContraseniaUser.Checked = false;

            rbActivoUsuarioSi.Checked = u.activo == 1;
            rbActivoUsuarioNo.Checked = u.activo == 0;
            rbMasculinoUsuario.Checked = u.sexo == "Masculino";
            rbFemeninoUsuario.Checked = u.sexo == "Femenino";
            rbOtrosUsuario.Checked = u.sexo == "Otros";

            //if (!u.fecha_nacimiento.HasValue)
            //    fechaNacimientoUsuario.CustomFormat = " ";
            //else
            //    fechaNacimientoUsuario.CustomFormat = "dd/MM/yyyy";

            if (!u.fecha_nacimiento.HasValue)
            {
                fechaNacimientoUsuario.Checked = false;
                fechaNacimientoUsuario.CustomFormat = " ";
            }
            else
            {
                fechaNacimientoUsuario.Checked = true;
                fechaNacimientoUsuario.CustomFormat = "dd/MM/yyyy";
                fechaNacimientoUsuario.Value = u.fecha_nacimiento.Value;
            }

            tbTelefonoUsuario.Text = u.telefono?.ToString() ?? string.Empty;
            tbDniUsuario.Text = u.dni.ToString();

            cbPerfilUsuario.SelectedValue = u.id_perfil;
            gbActivoUsuario.Visible = (u.activo == 0);

            // Habilita “Eliminar” solo si activa == 1
            btnBorrar.Enabled = u.activo == 1;
        }
        private void Busqueda_TextChanged(object sender, EventArgs e)
        {
            FiltrarYRefrescar();
        }

        private void FiltrarYRefrescar()
        {
            var perfiles = ObtenerPerfiles();
            var usuarios = ObtenerUsuarios();

            // Se aplica un filtro por activo/inactivo
            var filtrados = usuarios.Where(u => (cbActivosUsuarios.Checked && u.activo == 1) || (cbInactivosUsuarios.Checked && u.activo == 0)).ToList();

            // Si hay texto en el DNI, filtrar por prefijo
            var textoDni = tbBusquedaDniUsuario.Text.Trim();
            if (textoDni.Length > 0 && textoDni.All(char.IsDigit))
            {
                filtrados = filtrados.Where(u => u.dni.ToString().StartsWith(textoDni)).ToList();
            }

            // Si hay texto en el Nombre, filtrar por contenido
            var textoNom = tbBusquedaNombreUsuario.Text.Trim();
            if (textoNom.Length > 0)
            {
                filtrados = filtrados.Where(u => u.nombre.IndexOf(textoNom, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            // Asignar la descripción de perfil
            foreach (var u in filtrados)
            {
                u.descripcion = perfiles.FirstOrDefault(p => p.id_perfil == u.id_perfil)?.descripcion ?? "Desconocido";
            }

            // Se refresca el DataGridView con la lista filtrada
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = filtrados;
        }

        private void LimpiarControles()
        {
            tbNombreUsuario.Clear();
            tbApellidoUsuario.Clear();
            tbEmailUsuario.Clear();
            tbContraseniaUsuario.Clear();
            tbContraseniaUsuario.UseSystemPasswordChar = true;
            cbOcultalContraseniaUser.Checked = false;
            tbTelefonoUsuario.Clear();
            tbDniUsuario.Clear();
            rbActivoUsuarioSi.Checked = true;
            rbActivoUsuarioNo.Checked = false;
            rbMasculinoUsuario.Checked = false;
            rbFemeninoUsuario.Checked = false;
            rbOtrosUsuario.Checked = true;
            fechaNacimientoUsuario.Checked = false;
            fechaNacimientoUsuario.CustomFormat = " ";
            cbPerfilUsuario.SelectedValue = 2;

            // Indica que ya no hay un usuario seleccionado
            _usuarioSeleccionadoId = -1;

            // Ocultar el groupbox en modo "nuevo usuario"
            gbActivoUsuario.Visible = false;
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Si no hay selección: limpio los campos
            if (_usuarioSeleccionadoId < 0)
            {
                LimpiarControles();
                return;
            }

            var dr = MessageBox.Show("¿Seguro que deseas desactivar este usuario?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            int filas = Usuario_controller.eliminarUsuario(_usuarioSeleccionadoId);

            if (filas == 1)
            {
                MessageBox.Show("Usuario desactivado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El usuario ya estaba desactivado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Se refresca el listado y se limpia
            FiltrarYRefrescar();
            LimpiarControles();
        }


        private void lbEmail_Click(object sender, EventArgs e)
        {

        }

        private void fechaNacimiento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbActivosUsuarios_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Filtro_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbActivosUsuarios.Checked && !cbInactivosUsuarios.Checked)
            {
                var cb = (CheckBox)sender;
                cb.Checked = true;
                return;
            }

            FiltrarYRefrescar();
        }

        private void tbBusquedaDniUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNombreUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private bool ValidarCampos()
        {
            // Nombre
            if (string.IsNullOrWhiteSpace(tbNombreUsuario.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNombreUsuario.Focus();
                return false;
            }

            // Apellido
            if (string.IsNullOrWhiteSpace(tbApellidoUsuario.Text))
            {
                MessageBox.Show("El apellido es obligatorio.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbApellidoUsuario.Focus();
                return false;
            }

            // Email
            string email = tbEmailUsuario.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(
                    "El email es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tbEmailUsuario.Focus();
                return false;
            }
            // Email Regex
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.com$", RegexOptions.IgnoreCase);
            if (!emailRegex.IsMatch(email))
            {
                MessageBox.Show(
                    "El email debe tener formato válido y terminar en .com.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tbEmailUsuario.Focus();
                return false;
            }

            // Contraseña
            string password = tbContraseniaUsuario.Text;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "La contraseña es obligatoria.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tbContraseniaUsuario.Focus();
                return false;
            }
            // Contraseña Regex
            var passRegex = new Regex(@"^(?=.{8,20}$)(?=.*[A-Z])(?=.*\d)[A-Za-z\d]+$");
            if (!passRegex.IsMatch(password))
            {
                MessageBox.Show(
                    "La contraseña debe tener entre 8 y 20 caracteres, " +
                    "al menos una letra mayúscula, un numero y ningún símbolo.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tbContraseniaUsuario.Focus();
                return false;
            }

            // Fecha de nacimiento
            if (fechaNacimientoUsuario.Checked)
            {
                DateTime fn = fechaNacimientoUsuario.Value.Date;
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - fn.Year;
                if (fn > hoy.AddYears(-edad))
                    edad--;

                if (edad < 18 || edad > 100)
                {
                    MessageBox.Show(
                        "La edad debe estar entre 18 y 100 años.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    fechaNacimientoUsuario.Focus();
                    return false;
                }
            }

            // DNI
            if (!int.TryParse(tbDniUsuario.Text, out int dni)
             || dni < 10_000_000 || dni > 99_999_999)
            {
                MessageBox.Show("El DNI debe ser un número entre 10.000.000 y 99.999.999.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDniUsuario.Focus();
                return false;
            }

            // Teléfono
            string txtTel = tbTelefonoUsuario.Text.Trim();
            if (!string.IsNullOrEmpty(txtTel))
            {
                if (!long.TryParse(txtTel, out long tel)
                 || tel < 1_000_000_000L
                 || tel > 9_999_999_999L)
                {
                    MessageBox.Show(
                        "El teléfono debe ser un número de 10 dígitos válido.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    tbTelefonoUsuario.Focus();
                    return false;
                }
            }

            // Perfil
            if (cbPerfilUsuario.SelectedValue == null)
            {
                MessageBox.Show("Debes seleccionar un perfil.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbPerfilUsuario.Focus();
                return false;
            }

            // Extraer valores para la comprobación
            int dniR = int.Parse(tbDniUsuario.Text);
            string emailR = tbEmailUsuario.Text.Trim();
            int idActR = _usuarioSeleccionadoId < 0 ? 0 : _usuarioSeleccionadoId;

            //  Última validación: DNI o email duplicados
            if (Usuario_controller.ExisteEmailODni(dniR, emailR, idActR))
            {
                MessageBox.Show("El DNI o el email ya están registrados en otro usuario.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDniUsuario.Focus();
                return false;
            }

            return true;
        }

        // Este método bloquea todo lo que no sea dígito o tecla de control
        private void TextBox_OnlyDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = char.IsControl(e.KeyChar);
            bool esDigito = char.IsDigit(e.KeyChar);

            if (!esControl && !esDigito)
            {
                e.Handled = true;
            }
        }

        // Permite solo letras y espacios
        private void TextBox_OnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = char.IsControl(e.KeyChar);
            bool esLetra = char.IsLetter(e.KeyChar);
            bool esEspacio = e.KeyChar == ' ';

            // Si NO es letra ni espacio se cancela la tecla
            if (!esControl && !esLetra && !esEspacio)
                e.Handled = true;
        }

        private void tbBusquedaNombreUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbOcultalContraseniaUser_CheckedChanged(object sender, EventArgs e)
        {
            tbContraseniaUsuario.UseSystemPasswordChar = !cbOcultalContraseniaUser.Checked;
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbTelefonoUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
