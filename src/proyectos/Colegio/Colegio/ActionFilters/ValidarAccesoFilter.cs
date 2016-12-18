﻿using Colegio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Colegio.ActionFilters
{
    public class ValidarAccesoFilter : ActionFilterAttribute
    {
        private readonly UsuarioNivel nivelMinimo;

        public ValidarAccesoFilter(UsuarioNivel nivel)
        {
            nivelMinimo = nivel;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var dbContext = new ColegioDbContext())
            {
                string usrConectado = filterContext.HttpContext.User.Identity.Name;

                var usuario = dbContext.Usuarios
                    .FirstOrDefault(u => u.NombreUsuario == usrConectado);

                if (usuario == null || usuario.Nivel > nivelMinimo)
                    filterContext.Result = new RedirectResult("http://google.com");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}