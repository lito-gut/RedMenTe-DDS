using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedMenTeWeb.Models
{
    public class Voluntario
    {
        public int VoluntarioID { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}