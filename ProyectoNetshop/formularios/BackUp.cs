using Microsoft.Data.SqlClient;
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


namespace vistaDeProyectoC
{
    public partial class FBackUp : Form
    {
        // Estado interno
        private string? _cadenaConexion = null;
        private bool _conectado = false;
        public FBackUp()
        {
            InitializeComponent();

            // Estado inicial de controles
            TBBaseDeDatos.Clear();
            TBGuardarRuta.Clear();
            TBGuardarRuta.Enabled = false;    // no se puede editar hasta conectar
            TBGuardarRuta.ReadOnly = true;    // evita escribir manualmente la ruta
            BConectar.Enabled = false;
            BGuardarRuta.Enabled = false;
            BBackUp.Enabled = false;

            // Suscripciones: quitar antes de agregar para evitar duplicados si el diseñador ya las asignó
            TBBaseDeDatos.TextChanged -= Campos_TextChanged;
            TBGuardarRuta.TextChanged -= Campos_TextChanged;
            BConectar.Click -= BConectar_Click;
            BGuardarRuta.Click -= BGuardarRuta_Click;
            BBackUp.Click -= BBackUp_Click;

            TBBaseDeDatos.TextChanged += Campos_TextChanged;
            TBGuardarRuta.TextChanged += Campos_TextChanged;
            BConectar.Click += BConectar_Click;
            BGuardarRuta.Click += BGuardarRuta_Click;
            BBackUp.Click += BBackUp_Click;
        }

        private void FBackUp_Load(object sender, EventArgs e)
        {
            TBBaseDeDatos.Text = "proyectoT";
            TBBaseDeDatos.ReadOnly = true; // 🔒 Bloquea edición manual
            Campos_TextChanged(this, EventArgs.Empty); // Actualiza botones

            TBGuardarRuta.ReadOnly = true;     // Evita escritura manual
            TBGuardarRuta.Enabled = true;      // Permite que se vea y se actualice desde código
        }

        private void TBBaseDeDatos_TextChanged(object sender, EventArgs e)
        {

        }

        private void Campos_TextChanged(object sender, EventArgs e)
        {
            // Habilitar conectar solo si hay texto en el textbox de BD
            BConectar.Enabled = !string.IsNullOrWhiteSpace(TBBaseDeDatos.Text);

            // BGuardarRuta habilitado únicamente cuando ya esté conectada la base
            BGuardarRuta.Enabled = _conectado;

            // Permitir editar/pegar la ruta sólo si _conectado (opcional)
            TBGuardarRuta.ReadOnly = true;
            TBGuardarRuta.Enabled = _conectado;

            // Habilitar BackUp solo si hay conexión establecida y ya se seleccionó una ruta
            BBackUp.Enabled = _conectado && !string.IsNullOrWhiteSpace(TBGuardarRuta.Text);
        }

        private void BConectar_Click(object sender, EventArgs e)
        {
            var nombreBD = TBBaseDeDatos.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombreBD))
            {
                MessageBox.Show("Ingresá el nombre de la base de datos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Construir cadena de conexión; adaptá Server si tu instancia es distinta
            _cadenaConexion = $"Server=localhost;Database={nombreBD};Trusted_Connection=True;TrustServerCertificate=True;";

            try
            {
                using (var conn = new SqlConnection(_cadenaConexion))
                {
                    conn.Open();
                    conn.Close();
                }

                _conectado = true;
                // Habilitar selección/edición de ruta
                TBGuardarRuta.Enabled = true;
                TBGuardarRuta.ReadOnly = true;
                MessageBox.Show($"Conexión exitosa a la base '{nombreBD}'.", "Conectado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _conectado = false;
                TBGuardarRuta.Enabled = false;
                TBGuardarRuta.ReadOnly = true;
                MessageBox.Show("No se pudo conectar: " + ex.Message, "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Campos_TextChanged(this, EventArgs.Empty);
        }

        private void TBGuardarRuta_TextChanged(object sender, EventArgs e)
        {

        }

        private void BGuardarRuta_Click(object sender, EventArgs e)
        {
            if (!_conectado)
            {
                MessageBox.Show("Primero conectate a la base de datos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Backup SQL Server (*.bak)|*.bak|Todos los archivos (*.*)|*.*";
                sfd.FileName = $"{TBBaseDeDatos.Text.Trim()}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                sfd.Title = "Guardar backup como...";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    TBGuardarRuta.Text = sfd.FileName;
                    MessageBox.Show($"Ruta seleccionada: {TBGuardarRuta.Text}", "Ruta guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            Campos_TextChanged(this, EventArgs.Empty);
        }

        private async void BBackUp_Click(object? sender, EventArgs e)
        {
            if (!_conectado || string.IsNullOrWhiteSpace(TBBaseDeDatos.Text) || string.IsNullOrWhiteSpace(TBGuardarRuta.Text))
            {
                MessageBox.Show("Antes de realizar el backup, conectá la base y elegí una ruta válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nombreBD = TBBaseDeDatos.Text.Trim();
            var archivo = TBGuardarRuta.Text.Trim();

            var dr = MessageBox.Show($"Se generará un BACKUP de la base '{nombreBD}' en:\n{archivo}\n¿Continuar?", "Confirmar Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            var comandoBackup = $"BACKUP DATABASE [{nombreBD}] TO DISK = N'{archivo}' WITH INIT;";

            try
            {
                using (var conn = new SqlConnection(_cadenaConexion!))
                using (var cmd = new SqlCommand(comandoBackup, conn))
                {
                    conn.Open();
                    cmd.CommandTimeout = 60 * 10; // 10 minutos
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }

                // Éxito: notificar, limpiar campos y resetear estado
                MessageBox.Show("Backup completado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                //TBBaseDeDatos.Clear();
                TBBaseDeDatos.Text = "proyectoT";
                TBGuardarRuta.Clear();

                // Resetear estado interno
                _cadenaConexion = null;
                _conectado = false;

                // Devolver controles al estado inicial
                TBGuardarRuta.Enabled = false;
                TBGuardarRuta.ReadOnly = true;
                BConectar.Enabled = false;
                BGuardarRuta.Enabled = false;
                BBackUp.Enabled = false;

                // Actualizar el enable/disable usando la lógica centralizada
                Campos_TextChanged(this, EventArgs.Empty);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error en SQL: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
