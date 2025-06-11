using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace RedMenTeWeb.Controllers
{
    public class CursoController : Controller
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

        public ActionResult Index()
        {
            var cursos = new List<CursosModel>();

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT CursoID, Nombre, Descripcion, Requisitos, FechaInicio, FechaFin FROM Cursos";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cursos.Add(new CursosModel
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

            return View(cursos);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(CursosModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = @"INSERT INTO Cursos (Nombre, Descripcion, Requisitos, FechaInicio, FechaFin) 
                                 VALUES (@Nombre, @Descripcion, @Requisitos, @FechaInicio, @FechaFin)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                cmd.Parameters.AddWithValue("@Requisitos", model.Requisitos);
                cmd.Parameters.AddWithValue("@FechaInicio", model.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", model.FechaFin);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("CrearCursoExito");
        }

        public ActionResult CrearCursoExito()
        {
            return View();
        }

        public ActionResult Editar(int id)
        {
            CursosModel curso = null;

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT CursoID, Nombre, Descripcion, Requisitos, FechaInicio, FechaFin FROM Cursos WHERE CursoID = @CursoID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CursoID", id);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    curso = new CursosModel
                    {
                        CursoID = Convert.ToInt32(reader["CursoID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Requisitos = reader["Requisitos"].ToString(),
                        FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                        FechaFin = Convert.ToDateTime(reader["FechaFin"])
                    };
                }
            }

            if (curso == null) return HttpNotFound();

            return View(curso);
        }

        [HttpPost]
        public ActionResult Editar(CursosModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = @"UPDATE Cursos 
                                 SET Nombre = @Nombre, Descripcion = @Descripcion, Requisitos = @Requisitos, 
                                     FechaInicio = @FechaInicio, FechaFin = @FechaFin
                                 WHERE CursoID = @CursoID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                cmd.Parameters.AddWithValue("@Requisitos", model.Requisitos);
                cmd.Parameters.AddWithValue("@FechaInicio", model.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", model.FechaFin);
                cmd.Parameters.AddWithValue("@CursoID", model.CursoID);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            CursosModel curso = null;

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT CursoID, Nombre FROM Cursos WHERE CursoID = @CursoID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CursoID", id);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    curso = new CursosModel
                    {
                        CursoID = Convert.ToInt32(reader["CursoID"]),
                        Nombre = reader["Nombre"].ToString()
                    };
                }
            }

            if (curso == null) return HttpNotFound();

            return View(curso);
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "DELETE FROM Cursos WHERE CursoID = @CursoID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CursoID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
