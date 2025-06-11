using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace RedMenTeWeb.Data
{
    public class CursoDao
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ProyectoDbConnection"].ConnectionString;

        public List<Curso> BuscarCurso(string filtro)
        {
            List<Curso> cursos = new List<Curso>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query;

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    // Si no hay filtro, mostramos todos los cursos
                    query = @"SELECT CursoID, Nombre, Descripcion, Requisitos, FechaInicio, FechaFin FROM Cursos";
                }
                else
                {
                    // Si hay filtro, aplicamos condiciones
                    query = @"SELECT CursoID, Nombre, Descripcion, Requisitos, FechaInicio, FechaFin
                              FROM Cursos 
                              WHERE Nombre LIKE @filtro OR Descripcion LIKE @filtro OR CAST(CursoID AS NVARCHAR) = @filtroExacto";
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
                        cursos.Add(new Curso()
                        {
                            CursoID = Convert.ToInt32(reader["CursoID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Requisitos = reader["Requisitos"].ToString(),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(reader["FechaFin"])
                        });
                    }
                }
            }
            return cursos;
        }

        public void AgregarCurso(Curso curso)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Cursos (Nombre, Descripcion, Requisitos, FechaInicio, FechaFin) 
                                 VALUES (@Nombre, @Descripcion, @Requisitos, @FechaInicio, @FechaFin)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", curso.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", curso.Descripcion);
                        cmd.Parameters.AddWithValue("@Requisitos", curso.Requisitos);
                        cmd.Parameters.AddWithValue("@FechaInicio", curso.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", curso.FechaFin);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al agregar el curso: " + ex.Message);
            }
        }

        public void ActualizarCurso(Curso curso)
        {
            // Actualiza un curso existente en la base de datos
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Consulta SQL para actualizar un curso
                string query = @"UPDATE Cursos 
                                 SET Nombre = @Nombre, Descripcion = @Descripcion, Requisitos = @Requisitos, 
                                     FechaInicio = @FechaInicio, FechaFin = @FechaFin 
                                 WHERE CursoID = @CursoID";
                // Asignamos los parámetros al comando
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CursoID", curso.CursoID);
                    cmd.Parameters.AddWithValue("@Nombre", curso.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", curso.Descripcion);
                    cmd.Parameters.AddWithValue("@Requisitos", curso.Requisitos);
                    cmd.Parameters.AddWithValue("@FechaInicio", curso.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", curso.FechaFin);
                    // Abrimos la conexión y ejecutamos el comando
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarCurso(int cursoId)
        {
            // Elimina un curso de la base de datos
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Consulta SQL para eliminar un curso
                string query = @"DELETE FROM Cursos WHERE CursoID = @CursoID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CursoID", cursoId);
                    // Abrimos la conexión y ejecutamos el comando
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}