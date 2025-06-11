using RedMenTeWeb.Data;
using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace RedMenTeWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuarioDao usuarioDao = new UsuarioDao();

        // GET: Usuarios
        public ActionResult Index(string filtro = "", int pagina = 1)
        {
            const int registrosPorPagina = 12;
            var todosLosUsuarios = usuarioDao.BuscarUsuario(filtro ?? "");

            // Paginación manual
            var usuariosPaginados = todosLosUsuarios
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToList();

            int totalRegistros = todosLosUsuarios.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Filtro = filtro;
            // Pasar los datos a la vista
            return View("ProfileAdmin", usuariosPaginados); 
        }


        // GET: Usuarios/Editar/5
        public ActionResult Editar(int id)
        {
            var usuarios = usuarioDao.BuscarUsuario(""); // Busca todos los usuarios
            UsuarioModel usuario = usuarios.Find(u => u.UsuarioID == id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Editar/5 
        [HttpPost]
        public ActionResult Editar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                usuarioDao.ActualizarUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // POST: Usuarios/Desactivar/5
        [HttpPost]
        public ActionResult Desactivar(int id)
        {
            usuarioDao.DesactivarUsuario(id);
            return RedirectToAction("Index");
        }

        // Método para probar la conexión
        public string ProbarConexion()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProyectoDbConnection"].ConnectionString))
                {
                    conn.Open();
                    return "Conexión exitosa a la base de datos.";
                }
            }
            catch (Exception ex)
            {
                return "Error de conexión: " + ex.Message;
            }
        }

        // GET: Usuarios/Admin
        public ActionResult Admin(string filtro = "")
        {
            filtro = filtro ?? ""; // Evitar null en filtro
            List<UsuarioModel> usuarios = usuarioDao.BuscarUsuario(filtro);
            ViewBag.Filtro = filtro;
            return View("ProfileAdmin", usuarios); // Usa la misma vista ProfileAdmin.cshtml para Admin
        }
    }
}
