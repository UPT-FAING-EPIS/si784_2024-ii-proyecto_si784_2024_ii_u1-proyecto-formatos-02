using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models;

namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Controllers
{
    public class TransferenciaController : Controller
    {
        private transferencia objTransferencia = new transferencia();
        // GET: Transferencia
        public ActionResult Index()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            string username= Session["Username"] as string;
            string usertype= Session["Usertype"] as string;
            double saldo= Convert.ToDouble(Session["Saldo"]);

            ViewBag.Username= username;
            ViewBag.Saldo= saldo;
            
            return View();

        }

        public ActionResult ListarTransferencias()
        {
            // Obtener el ID del usuario desde la sesión
            int userId = Convert.ToInt32(Session["UserID"]);

            // Llamar al método ListaTransferencia con el ID del usuario
            List<transferencia.TransferenciaResult> transferenciaResults = objTransferencia.ListaTransferencia(userId);

            return View(transferenciaResults);
        }

        public ActionResult DetalleTransferencia(int id) { 
            return View(objTransferencia.Detalle(id));
        }

        [HttpGet]
        public ActionResult Transferir()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserId = Session["UserID"];
            return View();
        }

        [HttpPost]
        public ActionResult Transferir(int IdEmisor, int IdReceptor, decimal Cantidad)
        {
            try
            {
                using (var db = new ModeloSistema())
                {
                    var emisor = db.usuarios.SingleOrDefault(u => u.id_usuario == IdEmisor);
                    var receptor = db.usuarios.SingleOrDefault(u => u.id_usuario == IdReceptor);

                    if (emisor == null || receptor == null)
                    {
                        TempData["ErrorMessage"] = "Emisor o receptor no encontrado.";
                        return RedirectToAction("Index");
                    }

                    if (emisor.saldo < Cantidad)
                    {
                        TempData["ErrorMessage"] = "Saldo insuficiente.";
                        return RedirectToAction("Index");
                    }

                    var transferencia = new transferencia
                    {
                        id_emisor = IdEmisor,
                        id_receptor = IdReceptor,
                        cantidad = Cantidad,
                        estado = 1, 
                        fecha = DateTime.Now
                    };

                    db.transferencias.Add(transferencia);

                    emisor.saldo -= Cantidad;
                    receptor.saldo += Cantidad;

                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Transferencia realizada con éxito.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al realizar la transferencia: " + ex.Message;
            }

            return RedirectToAction("Transferir");
        }

    }
}