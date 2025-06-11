using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedMenTeWeb.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #region Acceso
        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpGet]

        public ActionResult RecuperarContrasenna()
        {
            return View();
        }
        #endregion

        #region Usuario
        [HttpGet]
        public ActionResult Proyectos()
        {

            return View();
        }

        [HttpGet]
        public ActionResult RecordatorioProgreso()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Calendario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Foro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DetalleForo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CrearPublicacion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MisCertificados()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DetalleCertificados()
        {
            return View();
        }
        #endregion

        [HttpGet]
        public ActionResult Seguridad()
        {
            return View();
        }
        #region Admin
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Estadisticas()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GestionProyectos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Patrocinadores()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GestionForo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GestionCertificados()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Participantes()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GestionCalendario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Voluntarios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProfileAdmin()
        {
            var Usuarios = new List<UsuarioModel>(); 
            return View("ProfileAdmin", Usuarios);
        }

        #endregion


        #region Evaluador

        [HttpGet]
        public ActionResult Evaluadores()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Comparaciones()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Retroalimentacion()
        {
            return View();
        }

        #endregion
    }
}