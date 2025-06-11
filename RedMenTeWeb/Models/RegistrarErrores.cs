﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedMenTeWeb.Models
{
    public class RegistroErrores
    {
        public void RegistrarError(string Mensaje, string Origen)
        {
            using (var context = new ())
            {
                var IdUsuario = (HttpContext.Current.Session["IdUsuario"] != null ? HttpContext.Current.Session["IdUsuario"].ToString() : "0");

                context.RegistrarError(long.Parse(IdUsuario), Mensaje, Origen);
            }
        }

    }
}