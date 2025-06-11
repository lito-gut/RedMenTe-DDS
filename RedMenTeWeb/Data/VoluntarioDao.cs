using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace RedMenTeWeb.Data
{
    public class VoluntarioDao
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ProyectoDbConnection"].ConnectionString;

        public List<Voluntario> BuscarVoluntario(string filtro)
        {
            List<Voluntario> voluntarios = new List<Voluntario>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query;

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    query = @"SELECT VoluntarioID, Nombre, Estado FROM Voluntarios";
                }
                else
                {
                    query = @"SELECT VoluntarioID, Nombre, Estado
                              FROM Voluntarios
                              WHERE Nombre LIKE @filtro OR CAST(VoluntarioID AS NVARCHAR) = @filtroExacto";
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
                        voluntarios.Add(new Voluntario()
                        {
                            VoluntarioID = Convert.ToInt32(reader["VoluntarioID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                }
            }

            return voluntarios;
        }

        public void AgregarVoluntario(Voluntario voluntario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"
                INSERT INTO Voluntarios (Nombre, Estado)
                VALUES (@Nombre, @Estado);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", voluntario.Nombre);
                        cmd.Parameters.AddWithValue("@Estado", voluntario.Estado);
                        conn.Open();

                        // Ejecuta el INSERT y devuelve el ID generado
                        int nuevoId = (int)cmd.ExecuteScalar();
                        voluntario.VoluntarioID = nuevoId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el voluntario: " + ex.Message);
            }
        }


        public void ActualizarVoluntario(Voluntario voluntario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Voluntarios
                                 SET Nombre = @Nombre, Estado = @Estado
                                 WHERE VoluntarioID = @VoluntarioID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VoluntarioID", voluntario.VoluntarioID);
                    cmd.Parameters.AddWithValue("@Nombre", voluntario.Nombre);
                    cmd.Parameters.AddWithValue("@Estado", voluntario.Estado);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarVoluntario(int voluntarioId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Voluntarios WHERE VoluntarioID = @VoluntarioID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VoluntarioID", voluntarioId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
