using Microsoft.Data.SqlClient;
using ProyectoNetshop.BD;
using ProyectoNetshop.Cruds;
using ProyectoNetshop.Cruds;
using ProyectoNetshop.formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vistaDeProyectoC
{
    public partial class FReporteUsuario : Form
    {
        public FReporteUsuario()
        {
            InitializeComponent();

            BGenerarReporteUsuario.Click -= BGenerarReporteUsuario_Click;
            BGenerarReporteUsuario.Click += BGenerarReporteUsuario_Click;

            // Cada vez que se cambie el texto de búsqueda por DNI se aplica el filtro a la grilla
            tbBusquedaDniUsuario.TextChanged += Busqueda_TextChanged;
            tbBusquedaNombreUsuario.TextChanged += Busqueda_TextChanged;

            // Solo letras (y espacios) para el nombre
            tbBusquedaNombreUsuario.KeyPress += TextBox_OnlyLetters_KeyPress;
            // Solo dígitos para el DNI
            tbBusquedaDniUsuario.KeyPress += TextBox_OnlyDigits_KeyPress;

        }

        private void FReporteUsuario_Load(object sender, EventArgs e)
        {
            //ocultar controles de filtro y búsqueda hasta que se genere el reporte
            cbActivosUsuarios.Visible = false;
            cbInactivosUsuarios.Visible = false;
            lbBuscadorUsuario.Visible = false;
            dgvUsuarios.Visible = false;
            tbBusquedaDniUsuario.Visible = false;
            tbBusquedaNombreUsuario.Visible = false;
            lbAdministradores.Visible = false;
            lbGerentes.Visible = false;
            lbVendedores.Visible = false;
            tbAdministradores.Visible = false;
            tbGerentes.Visible = false;
            tbVendedores.Visible = false;

            cbActivosUsuarios.Enabled = false;
            cbInactivosUsuarios.Enabled = false;

            cbActivosUsuarios.CheckedChanged += Filtro_CheckedChanged;
            cbInactivosUsuarios.CheckedChanged += Filtro_CheckedChanged;

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
            //dgvUsuarios.DataSource = usuarios;
            //dgvReporteUsuarios.Visible = false;
        }

        private List<Perfil_model> ObtenerPerfiles()
        {
            var lista = new List<Perfil_model>();

            using (SqlConnection conexion = ProyectoNetshop.BD.BaseDeDatos.obtenerConexion())
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
            using var con = ProyectoNetshop.BD.BaseDeDatos.obtenerConexion();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"SELECT id_usuario, nombre, apellido, email, activo, sexo, fecha_nacimiento, telefono, dni, id_perfil FROM usuario";
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DateTime? fn = rd.IsDBNull(6)
                    ? (DateTime?)null
                    : rd.GetDateTime(6);
                //long? tel = rd.IsDBNull(8) ? (long?)null : rd.GetInt64(8);
                // telefono: manejar bigint o varchar en la DB
                long? tel = null;
                if (!rd.IsDBNull(7))
                {
                    var fieldType = rd.GetFieldType(7);
                    if (fieldType == typeof(long) || fieldType == typeof(Int64))
                    {
                        tel = rd.GetInt64(7);
                    }
                    else
                    {
                        // intentar leer como string y parsear de forma segura
                        string telStr = rd.GetString(7);
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
                    activo = rd.GetInt32(4),
                    sexo = rd.GetString(5),
                    fecha_nacimiento = fn,
                    telefono = tel,
                    dni = rd.GetInt32(8),
                    id_perfil = rd.GetInt32(9)
                };

                lista.Add(usuario);
            }

            return lista;
        }

        private void BGenerarReporteUsuario_Click(object sender, EventArgs e)
        {
            // Mostrar los controles de filtro y búsqueda
            cbActivosUsuarios.Visible = true;
            cbInactivosUsuarios.Visible = true;
            lbBuscadorUsuario.Visible = true;
            dgvUsuarios.Visible = true;
            tbBusquedaDniUsuario.Visible = true;
            tbBusquedaNombreUsuario.Visible = true;
            lbAdministradores.Visible = true;
            lbGerentes.Visible = true;
            lbVendedores.Visible = true;
            tbAdministradores.Visible = true;
            tbGerentes.Visible = true;
            tbVendedores.Visible = true;

            // Se activan los checkboxes de filtro
            cbActivosUsuarios.Enabled = true;
            cbInactivosUsuarios.Enabled = true;

            // Por defecto muestro ambos
            cbActivosUsuarios.Checked = true;
            cbInactivosUsuarios.Checked = true;

            // Obtener la lista de perfiles
            var perfiles = ObtenerPerfiles();

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

            dgvUsuarios.DataSource = usuarios;

            // Calcular totales por rol (según id_perfil: 1=Admin, 2=Vendedor, 3=Gerente)
            int totalAdministradores = usuarios.Count(u => u.id_perfil == 1 && u.activo == 1);
            int totalGerentes = usuarios.Count(u => u.id_perfil == 2 && u.activo == 1);
            int totalVendedores = usuarios.Count(u => u.id_perfil == 3 && u.activo == 1);
            
            // Mostrar totales en los TextBox (siempre como cadena)
            tbAdministradores.Text = totalAdministradores.ToString();
            tbGerentes.Text = totalGerentes.ToString();
            tbVendedores.Text = totalVendedores.ToString();

            MessageBox.Show("Usuarios cargados con éxito.", "Reporte de Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Busqueda_TextChanged(object sender, EventArgs e)
        {
            FiltrarYRefrescar();
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
    }
}
