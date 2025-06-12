using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedMenTeWeb.Models;

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

        #endregion
        public static List<ContribucionProyectoModel> listaContribuciones = new List<ContribucionProyectoModel>
{
    new ContribucionProyectoModel { Id = 1, Proyecto = "Energía Renovable", Monto = "$10,000", Impacto = "40%" },
    new ContribucionProyectoModel { Id = 2, Proyecto = "Innovación Educativa", Monto = "$8,000", Impacto = "35%" },
    new ContribucionProyectoModel { Id = 3, Proyecto = "Tecnología Avanzada", Monto = "$5,000", Impacto = "25%" }
};

        

        [HttpPost]
        public ActionResult AgregarContribucion(ContribucionProyectoModel modelo)
        {
            modelo.Id = listaContribuciones.Count > 0 ? listaContribuciones.Max(c => c.Id) + 1 : 1;
            listaContribuciones.Add(modelo);
            return RedirectToAction("Patrocinadores");
        }

        public ActionResult EliminarContribucion(int id)
        {
            var item = listaContribuciones.FirstOrDefault(c => c.Id == id);
            if (item != null) listaContribuciones.Remove(item);
            return RedirectToAction("Patrocinadores");
        }

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