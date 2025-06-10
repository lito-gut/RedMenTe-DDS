using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RedMenTeWeb.Models
{
    public class UsuarioModel
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public int Edad { get; set; }
        public int ProgramaID { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; } // Activo o Inactivo
    }
}