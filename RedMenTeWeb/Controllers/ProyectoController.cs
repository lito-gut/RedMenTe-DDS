using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace RedMenTeWeb.Controllers
{
    public class ProyectoController : Controller
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

        public ActionResult Index()
        {
            var proyectos = new List<ProyectosModel>();

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT ProyectoID, Nombre, Descripcion, ODS, Estado, FechaInicio, FechaFin FROM Proyectos";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    proyectos.Add(new ProyectosModel
                    {
                        ProyectoID = Convert.ToInt32(reader["ProyectoID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        ODS = reader["ODS"].ToString(),
                        Estado = reader["Estado"].ToString(),
                        FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                        FechaFin = Convert.ToDateTime(reader["FechaFin"])
                    });
                }
            }

            return View(proyectos);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProyectosModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = @"INSERT INTO Proyectos (Nombre, Descripcion, ODS, Estado, FechaInicio, FechaFin) 
                 VALUES (@Nombre, @Descripcion, @ODS, @Estado, @FechaInicio, @FechaFin)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                cmd.Parameters.AddWithValue("@ODS", model.ODS);
                cmd.Parameters.AddWithValue("@Estado", model.Estado);
                cmd.Parameters.AddWithValue("@FechaInicio", model.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", model.FechaFin);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("CrearProyectoExito");
        }

        public ActionResult CrearProyectoExito()
        {
            return View();
        }

        public ActionResult Editar(int id)
        {
            ProyectosModel proyecto = null;

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT ProyectoID, Nombre, Descripcion, ODS, Estado, FechaInicio, FechaFin FROM Proyectos WHERE ProyectoID = @ProyectoID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProyectoID", id);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    proyecto = new ProyectosModel
                    {
                        ProyectoID = Convert.ToInt32(reader["ProyectoID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        ODS = reader["ODS"].ToString(),
                        Estado = reader["Estado"].ToString(),
                        FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                        FechaFin = Convert.ToDateTime(reader["FechaFin"])
                    };
                }
            }

            if (proyecto == null) return HttpNotFound();

            return View(proyecto);
        }

        [HttpPost]
        public ActionResult Editar(ProyectosModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = @"UPDATE Proyectos 
                                 SET Nombre = @Nombre, Descripcion = @Descripcion, ODS = @ODS, Estado = @Estado, 
                                     FechaInicio = @FechaInicio, FechaFin = @FechaFin
                                 WHERE ProyectoID = @ProyectoID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                cmd.Parameters.AddWithValue("@ODS", model.ODS);
                cmd.Parameters.AddWithValue("@Estado", model.Estado);
                cmd.Parameters.AddWithValue("@FechaInicio", model.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", model.FechaFin);
                cmd.Parameters.AddWithValue("@ProyectoID", model.ProyectoID);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            ProyectosModel proyecto = null;

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT ProyectoID, Nombre FROM Proyectos WHERE ProyectoID = @ProyectoID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProyectoID", id);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    proyecto = new ProyectosModel
                    {
                        ProyectoID = Convert.ToInt32(reader["ProyectoID"]),
                        Nombre = reader["Nombre"].ToString()
                    };
                }
            }

            if (proyecto == null) return HttpNotFound();

            return View(proyecto);
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "DELETE FROM Proyectos WHERE ProyectoID = @ProyectoID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProyectoID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
