using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ProyectoNetshop.BD
{
    internal class BaseDeDatos
    {
        public static SqlConnection obtenerConexion()
        {
            //SqlConnection conexion = new SqlConnection("Server=LAPTOP-KCCIPSRT\\SQLEXPRESS;Database=proyectoT;Trusted_Connection=True;TrustServerCertificate=True;");
            SqlConnection conexion = new SqlConnection("Server=localhost;Database=proyectoT;Trusted_Connection=True;TrustServerCertificate=True;");

            conexion.Open();

            return conexion;
        }
    }
}