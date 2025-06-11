using RedMenTeWeb.Data;
using RedMenTeWeb.Models; 
using System;
using System.Linq;
using System.Web.Mvc;

namespace RedMenTeWeb.Controllers
{
    public class CursosController : Controller
    {

        private CursoDao cursoDao = new CursoDao();

        // GET: Cursos

        public ActionResult Index(string filtro = "", int pagina = 1)
        {
            const int registrosPorPagina = 4;
            var todosLosCursos = cursoDao.BuscarCurso(filtro ?? "");

            // Paginación manual
            var cursosPaginados = todosLosCursos
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToList();

            // Obtener el total de registros y calcular el total de páginas
            int totalRegistros = todosLosCursos.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);

            // Pasar los datos a la vista
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Filtro = filtro;
            return View("GestionProyectos", cursosPaginados); 
        }

        // GET: Cursos/Crear

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Cursos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Curso curso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // La funcion es AgregarCurso del CursoDao se encarga de insertar el curso en la base de datos
                    cursoDao.AgregarCurso(curso);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear el curso: " + ex.Message);
                }
            }
            // Si el modelo no es válido, volvemos a mostrar el formulario con los errores
            return View(curso);
        }

        // GET: Cursos/Editar/5
        public ActionResult Editar(int id)
        {
            // Busca todos los cursos
            var cursos = cursoDao.BuscarCurso("");
            Curso curso = cursos.Find(c => c.CursoID == id);

            // Si no se encuentra el curso, devolvemos un error 404
            if (curso == null)
            {
                return HttpNotFound();
            }
            // Devolvemos la vista de edición con el curso encontrado
            return View(curso);
        }

        // POST: Cursos/Editar/5
        [HttpPost]
        public ActionResult Editar(Curso curso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Actualiza el curso en la base de datos
                    cursoDao.ActualizarCurso(curso);
                    return RedirectToAction("", "Cursos");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar el curso: " + ex.Message);
                }
            }
            // Si el modelo no es válido, volvemos a mostrar el formulario con los errores
            return View(curso);
        }

        // GET: Cursos/Eliminar/5
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                // Elimina el curso de la base de datos
                cursoDao.EliminarCurso(id);
                return RedirectToAction("","Cursos");
            }
            catch (Exception ex)
            {
                // Si ocurre un error al eliminar, mostramos un mensaje de error
                ModelState.AddModelError("", "Error al eliminar el curso: " + ex.Message);
                return RedirectToAction("","Cursos");
            }
        }


        [HttpGet]
        public ActionResult GestionProyectos()
        {
            var cursos = cursoDao.BuscarCurso("");
            return View(cursos);
        }

    }
}