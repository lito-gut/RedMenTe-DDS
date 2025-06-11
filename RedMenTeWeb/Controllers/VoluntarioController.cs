using RedMenTeWeb.Data;
using RedMenTeWeb.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RedMenTeWeb.Controllers
{
    public class VoluntarioController : Controller
    {
        private VoluntarioDao voluntarioDao = new VoluntarioDao();

        // GET: Voluntario
        public ActionResult Buscar(string filtro = "", int pagina = 1)
        {
            const int registrosPorPagina = 4;
            var todosLosVoluntarios = voluntarioDao.BuscarVoluntario(filtro ?? "");

            var voluntariosPaginados = todosLosVoluntarios
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToList();

            int totalRegistros = todosLosVoluntarios.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Filtro = filtro;
            return View("Index", voluntariosPaginados);
        }

        // GET: Voluntario/Crear
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Voluntario/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    voluntarioDao.AgregarVoluntario(voluntario);

                    // Guardar el ID del voluntario recién registrado en la sesión
                    Session["VoluntarioID"] = voluntario.VoluntarioID;

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear el voluntario: " + ex.Message);
                }
            }

            // Cargar lista de voluntarios en caso de error para que no se pierda la vista
            ViewBag.Voluntarios = voluntarioDao.BuscarVoluntario("");
            return View("Index", voluntario);
        }


        // GET: Voluntario/Editar/5
        public ActionResult Editar(int id)
        {
            var voluntarios = voluntarioDao.BuscarVoluntario("");
            Voluntario voluntario = voluntarios.Find(v => v.VoluntarioID == id);

            if (voluntario == null)
            {
                return HttpNotFound();
            }

            return View(voluntario);
        }

        // POST: Voluntario/Editar/5
        [HttpPost]
        public ActionResult Editar(Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    voluntarioDao.ActualizarVoluntario(voluntario);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar el voluntario: " + ex.Message);
                }
            }
            return View(voluntario);
        }

        // POST: Voluntario/Eliminar/5
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            int? miVoluntarioID = Session["VoluntarioID"] as int?;
            if (miVoluntarioID == null || miVoluntarioID.Value != id)
            {
                // No permito eliminar si el id no coincide con el de la sesión
                return new HttpStatusCodeResult(403, "No autorizado para eliminar este voluntario.");
            }

            try
            {
                voluntarioDao.EliminarVoluntario(id);
                Session.Remove("VoluntarioID"); // Opcional, si quieres eliminar el id guardado
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el voluntario: " + ex.Message);
                return RedirectToAction("Index");
            }
        }


        // GET: Voluntario/Index
        public ActionResult Index()
        {
            var voluntarios = voluntarioDao.BuscarVoluntario(""); // Obtener todos los voluntarios
            ViewBag.Voluntarios = voluntarios; // Pasar al ViewBag

            return View(new Voluntario()); // Pasar un modelo vacío para el formulario
        }


    }
}
