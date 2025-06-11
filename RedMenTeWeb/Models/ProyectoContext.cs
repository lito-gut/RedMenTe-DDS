using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RedMenTeWeb.Models
{
    public class ProyectoContext : DbContext
    {
        public ProyectoContext() : base("ProyectoDbConnection")
        {
        }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}