using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Negocio
{
   public class N_Usuario
    {
        D_Usuario objd = new D_Usuario();

        public DataTable N_usuarios(E_Usuario obje)
        {
            return objd.D_usuario(obje);


        }
     

        private D_Usuario DATOSUSUARIO = new D_Usuario();
        public DataTable ObtenerUsuarios()
        {
            try
            {
                // Llama al método correspondiente en la capa de datos para obtener los usuarios
                return DATOSUSUARIO.ListarUsuarios();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al obtener los usuarios
                throw new Exception("Error al obtener los usuarios desde la capa de datos: " + ex.Message);
            }
        }


    }
}

