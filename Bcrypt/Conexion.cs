using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bcrypt
{

    using System;
    using System.Windows.Forms;
    using MySql.Data.MySqlClient;

    namespace Bcrypt
    {
        public class Conexion
        {
            private static string cadenaConexion = "Server=localhost;Database=Parcial3;Uid=root;Pwd=prima1008;";

            public static MySqlConnection ObtenerConexion()
            {
                try
                {
                    MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                    conexion.Open();
                    return conexion;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al conectar: " + ex.Message);
                    return null;
                }
            }
        }
    }
}