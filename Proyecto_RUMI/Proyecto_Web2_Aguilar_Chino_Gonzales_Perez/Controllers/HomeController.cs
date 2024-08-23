using Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private transferencia objTransferencia = new transferencia();

        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            string username = Session["Username"] as string;
            decimal saldo = Convert.ToDecimal(Session["Saldo"]);

            // Obtener las transferencias del usuario
            List<transferencia.TransferenciaResult> transferenciaResults = objTransferencia.Lista6Transferencia(userId);

            ViewBag.Username = username;
            ViewBag.UserID = userId;
            ViewBag.Saldo = saldo;
            ViewBag.FechaActual = DateTime.Now.ToString("dd MMMM, yyyy", new CultureInfo("es-ES"));

            return View(transferenciaResults);
        }

        public ActionResult ListarTransferencias()
        {
            // Verificar que la sesión tiene los datos necesarios
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserID"]);

            // Obtener las transferencias del usuario
            List<transferencia.TransferenciaResult> transferenciaResults = objTransferencia.ListaTransferencia(userId);

            return View(transferenciaResults);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Your test page.";

            return View();
        }

        
    }
}
