using System;

namespace RedMenTeWeb.Models
{
    public class ProyectosModel
    {
        public int ProyectoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ODS { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}