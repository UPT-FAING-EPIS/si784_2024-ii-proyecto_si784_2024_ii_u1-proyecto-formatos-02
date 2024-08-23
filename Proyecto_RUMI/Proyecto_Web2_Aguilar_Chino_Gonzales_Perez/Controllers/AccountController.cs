using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models;
using System.Web.Security;


namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Controllers
{
    public class AccountController : Controller
    {
        private ModeloSistema db = new ModeloSistema();

        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = db.usuarios.FirstOrDefault(u => u.usuario1 == model.UserName && u.clave == model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.usuario1, false);
                    Session["UserID"]=user.id_usuario;
                    Session["Username"] = user.usuario1;
                    Session["Usertype"] = user.tipo_usuario;
                    Session["Saldo"] = user.saldo;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); 
            return RedirectToAction("Login", "Account");
        }
    }
}