using Bcrypt.Bcrypt;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bcrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" && txtContrasena.Text != "")
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text;

                using (MySqlConnection cn = Conexion.ObtenerConexion())
                {
                    if (cn == null)
                        return;

                    int? idUsuario = null;
                    string nombre = "";
                    string hash = null;

                    string sql = @"SELECT id_usuario, nombre, password_hash
                           FROM usuarios
                           WHERE nombre = @usuario";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                idUsuario = Convert.ToInt32(dr["id_usuario"]);
                                nombre = dr["nombre"].ToString();
                                hash = dr["password_hash"].ToString();
                            }
                        }
                    }

                    bool acceso = hash != null && BCrypt.Net.BCrypt.Verify(contrasena, hash);

                    string sqlIntento = @"INSERT INTO intentos(id_usuario, usuario, intentoFallido,intentoExitoso, fechaIntento) 
                        VALUES (@id, @usuario, @fallido, @exitoso, NOW())";

                    using (MySqlCommand cmd = new MySqlCommand(sqlIntento, cn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@id", (object)idUsuario ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@fallido", acceso ? 0 : 1);
                        cmd.Parameters.AddWithValue("@exitoso", acceso ? 1 : 0);

                        cmd.ExecuteNonQuery();
                    }

                    if (acceso)
                    {
                        MessageBox.Show("Bienvenido, " + nombre,
                            "Acceso exitoso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.",
                            "Acceso denegado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }

                    txtContrasena.Clear();
                }
            }
            else
            {
                MessageBox.Show("Ingrese usuario y contraseña.");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (usuario == "" || contrasena == "")
            {
                MessageBox.Show("Ingrese usuario y contraseña.");
                return;
            }

            string hash = BCrypt.Net.BCrypt.HashPassword(contrasena);

            using (MySqlConnection cn = Conexion.ObtenerConexion())
            {
                if (cn == null)
                    return;

                string sql = @"INSERT INTO usuarios
                       (nombre, password_hash)
                       VALUES (@nombre, @hash)";

                using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nombre", usuario);
                    cmd.Parameters.AddWithValue("@hash", hash);

                    try
                    {
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Usuario registrado correctamente.");
                        txtUsuario.Clear();
                        txtContrasena.Clear();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("No se pudo registrar el usuario: "
                                        + ex.Message);
                    }
                }
            }
        }

    }
}