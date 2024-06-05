using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using System.Configuration;


namespace Datos
{
    public class D_Usuario
    {
       SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
        public DataTable D_usuario(E_Usuario obje)
        {
            SqlCommand cmd = new SqlCommand("sp_login", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usuario", obje.usuario);
            cmd.Parameters.AddWithValue("@Contraseña", obje.Contraseña);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }



        private readonly string connectionString = "Data Source=RADELIN-PC;Initial Catalog=Inicio_de_Sesion;Persist Security Info=True;User ID=sa;Password=12345678;";

        public string Guardar_Usuario(int estadoGuardado, E_Usuario usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GuardarUsuario3", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@Usuario", usuario.usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                        cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                        cmd.Parameters.AddWithValue("@Estado", usuario.Estado);

                        cmd.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message; // Devuelve el mensaje de error
            }
        }


        public string Eliminar_Usuario(int Id_Usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);

                        SqlParameter resultado = new SqlParameter("@Resultado", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultado);

                        cmd.ExecuteNonQuery();
                        return resultado.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message; // Devuelve el mensaje de error
            }
        }





        public DataTable ListarUsuarios()
        {
            DataTable dataTable = new DataTable(); // Crear un DataTable para almacenar los usuarios

            try
            {
                // Crear una nueva conexión utilizando la cadena de conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Definir la consulta SQL para obtener los usuarios
                    string query = "SELECT * FROM Usuario"; // Reemplaza 'Usuarios' con el nombre de tu tabla de usuarios

                    // Crear un SqlDataAdapter para ejecutar la consulta y llenar el DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Llenar el DataTable con los resultados de la consulta
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra y lanzarla nuevamente
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }

            // Devolver el DataTable con los usuarios
            return dataTable;
        }


    }
}
