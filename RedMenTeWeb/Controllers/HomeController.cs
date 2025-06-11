using RedMenTeWeb.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RedMenTeWeb.Models;

namespace RedMenTeWeb.Controllers
{
    public class HomeController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #region Acceso
        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            try
            { 
            return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RegistrarCuenta");
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioModel model)
        {
            return RedirectToAction("IniciarSesion", "Home");
        }


        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioModel model)
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RecuperarContrasenna");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioModel model)
            {
                try 
                {
                    using (var context = new ())
                    {
                        var info = context.Usuarios.FirstOrDefault(x => x.Correo == model.Correo && x.Estado == true);

                        if (info != null)
                        {
                            var codigoTemporal = CrearCodigo();
                            context.ActualizarContrasenna(model.Correo, codigoTemporal);

                            string mensaje = util.MensajeRecuperacion(info, codigoTemporal);
                            bool notificacion = util.EnviarCorreo(info, mensaje, "Acceso al sistema PetLover");

                            if (notificacion)
                                return RedirectToAction("IniciarSesion", "Home");
                        }

                        ViewBag.Mensaje = "Su acceso no se ha podido restablecer correctamente";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    error.RegistrarError(ex.Message, "Post ForgotPassword");
                    return View("Error");
                }
            }
           
            
            private string CrearCodigo()
            {
                int length = 5;
                const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 < length--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                return res.ToString();
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