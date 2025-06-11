using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedMenTeWeb.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; } 
    }
}