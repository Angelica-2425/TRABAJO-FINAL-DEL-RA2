using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using static Negocio.N_Usuario;
using Entidades;
using static System.Windows.Forms.AxHost;
using Datos;
using System.Data.Common;

namespace Presentacion
{
    public partial class Usuario : Form
    {

        private N_Usuario NegocioUsuario = new N_Usuario();
        private int estadoGuardado;
        private int ObtenerUsuarios;
        private int idUsuarioSeleccionado;

        public Usuario()
        {
            InitializeComponent();
            MostrarDatos();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            // Validar que todos los datos requeridos están ingresados
            if (string.IsNullOrWhiteSpace(txtnombrecompleto.Text) ||
                string.IsNullOrWhiteSpace(txtcorreo.Text) ||
                string.IsNullOrWhiteSpace(txtclave.Text) ||
                string.IsNullOrWhiteSpace(cborol.Text) ||
                string.IsNullOrWhiteSpace(cboestado.Text))
            {
                MessageBox.Show("Falta ingresar datos requeridos",
                    "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; // Salir del método si falta algún dato
            }

            // Crear una instancia de E_Usuario y asignar valores
            E_Usuario oUsuario = new E_Usuario
            {
                Nombre = txtnombrecompleto.Text,
                usuario = txtcorreo.Text,
                Contraseña = txtclave.Text,
                Rol = cborol.Text,
                Estado = cboestado.Text
            };

            // Crear una instancia de D_Usuario y guardar los datos
            D_Usuario d_Usuario = new D_Usuario();
            string respuesta = d_Usuario.Guardar_Usuario(this.estadoGuardado, oUsuario);

            if (respuesta == "OK")
            {
                MessageBox.Show("Los datos han sido guardados correctamente",
                                "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Realiza cualquier acción adicional si es necesario
            }
            else
            {
                MessageBox.Show("Error al guardar los datos: " + respuesta,
                                "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                int fila = dataGridView1.CurrentCell.RowIndex;
                txtdocumento.Text = dataGridView1[0, fila].Value.ToString();
                txtnombrecompleto.Text = dataGridView1[1, fila].Value.ToString();
                txtcorreo.Text = dataGridView1[2, fila].Value.ToString();
                txtclave.Text = dataGridView1[3, fila].Value.ToString();
                cborol.Text = dataGridView1[4, fila].Value.ToString();
                cboestado.Text = dataGridView1[5, fila].Value.ToString();
            }
        }

   

        private void btnlimpiar_Click(object sender, EventArgs e)
        {


            txtclave.Clear();
            txtcorreo.Clear();
            txtdocumento.Clear();
            txtnombrecompleto.Clear();
            
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            // Obtener el Id_Usuario que deseas eliminar (por ejemplo, desde un DataGridView)
            int idUsuarioAEliminar = ObtenerIdUsuarioSeleccionado();

            // Crear una instancia de D_Usuario y llamar al método Eliminar_Usuario
            D_Usuario d_Usuario = new D_Usuario();
            string respuesta = d_Usuario.Eliminar_Usuario(idUsuarioAEliminar);

            // Manejar la respuesta
            if (respuesta == "OK")
            {
                MessageBox.Show("El usuario ha sido eliminado correctamente",
                                "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Actualizar la interfaz de usuario o realizar otras acciones necesarias
            }
            else
            {
                MessageBox.Show("Error al eliminar el usuario: " + respuesta,
                                "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerIdUsuarioSeleccionado()
        {
            // Verificar si se ha seleccionado una fila en el DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el valor de la celda en la columna "IdUsuario" de la fila seleccionada
                return Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id_Usuario"].Value);
            }
            else
            {
                // Si no se ha seleccionado ninguna fila, devolver un valor predeterminado o lanzar una excepción
                throw new Exception("No se ha seleccionado ningún usuario.");
            }
        }

        private void MostrarDatos()
        {
            try
            {
                // Obtener los datos de usuarios desde la capa de negocio
                DataTable dataTable = NegocioUsuario.ObtenerUsuarios();

                // Asignar el DataTable al DataSource del DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                MessageBox.Show("Error al obtener los usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            MostrarDatos();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Usuario_Load(object sender, EventArgs e)
        {

        }
    }
    
}
    
