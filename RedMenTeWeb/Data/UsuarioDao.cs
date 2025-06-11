using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RedMenTeWeb.Data
{
    public class UsuarioDao
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ProyectoDbConnection"].ConnectionString;

        public List<UsuarioModel> BuscarUsuario(string filtro)
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query;

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    // Si no hay filtro, mostramos todos los usuarios
                    query = @"SELECT UsuarioID, Nombre, Correo, Rol, Edad, ProgramaID, Estado FROM Usuarios";
                }
                else
                {
                    // Si hay filtro, aplicamos condiciones
                    query = @"SELECT UsuarioID, Nombre, Correo, Rol, Edad, ProgramaID, Estado
                      FROM Usuarios 
                      WHERE Nombre LIKE @filtro OR Correo LIKE @filtro OR CAST(UsuarioID AS NVARCHAR) = @filtroExacto";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                    {
                        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                        cmd.Parameters.AddWithValue("@filtroExacto", filtro);
                    }

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        usuarios.Add(new UsuarioModel
                        {
                            UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            Edad = Convert.ToInt32(reader["Edad"]),
                            ProgramaID = Convert.ToInt32(reader["ProgramaID"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                }
            }

            return usuarios;
        }



        public void ActualizarUsuario(UsuarioModel usuario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //Consulta SQL para actualizar un usuario
                string query = @"UPDATE Usuarios 
                                 SET Nombre = @Nombre, Correo = @Correo, Rol = @Rol, Edad = @Edad, ProgramaID = @ProgramaID, Estado = @Estado
                                 WHERE UsuarioID = @UsuarioID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //Asignamos los parámetros al comando
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                    cmd.Parameters.AddWithValue("@ProgramaID", usuario.ProgramaID);
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                    //Abrimos la conexión y ejecutamos el comando
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DesactivarUsuario(int usuarioID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Consulta SQL para desactivar un usuario
                string query = @"UPDATE Usuarios SET Estado = 0 WHERE UsuarioID = @UsuarioID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Asignamos el parámetro al comando
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    // Abrimos la conexión y ejecutamos el comando
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}