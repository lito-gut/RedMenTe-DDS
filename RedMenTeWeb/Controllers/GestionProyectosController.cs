using RedMenTeWeb.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

public class AdminController : Controller
{
    private string cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

    public ActionResult Gestion()
    {
        var proyectos = new List<ProyectosModel>();
        var cursos = new List<CursosModel>();

        using (var conexion = new SqlConnection(cadenaConexion))
        {
            conexion.Open();

            // Cargar proyectos
            using (var cmdProyectos = new SqlCommand("SELECT Id, Nombre FROM proyectos", conexion))
            using (var reader = cmdProyectos.ExecuteReader())
            {
                while (reader.Read())
                {
                    proyectos.Add(new ProyectosModel
                    {
                        ProyectoID = reader.GetInt32(0),
                        Nombre = reader.GetString(1)
                    });
                }
            }

            // Cargar cursos
            using (var cmdCursos = new SqlCommand("SELECT Id, Nombre FROM cursos", conexion))
            using (var reader = cmdCursos.ExecuteReader())
            {
                while (reader.Read())
                {
                    cursos.Add(new CursosModel
                    {
                        CursoID = reader.GetInt32(0),
                        Nombre = reader.GetString(1)
                    });
                }
            }
        }

        // Creamos un ViewModel para pasar las listas a la vista
        var viewModel = new ProyectosCursosModel
        {
            Proyectos = proyectos,
            Cursos = cursos
        };

        return View("~/Views/Home/GestionProyectos.cshtml", viewModel);
    }
}

// ViewModel para pasar a la vista
    public class ProyectosCursosModel
    {
        public List<ProyectosModel> Proyectos { get; set; } = new List<ProyectosModel>();
        public List<CursosModel> Cursos { get; set; } = new List<CursosModel>();
    }