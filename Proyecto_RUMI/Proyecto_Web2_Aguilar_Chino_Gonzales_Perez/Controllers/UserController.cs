using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models;

namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Controllers
{
    public class UserController : Controller
    {
        private ModeloSistema db = new ModeloSistema();

        // GET: User
        public ActionResult Index()
        {
            return View(db.usuarios.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario user = db.usuarios.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.TipoUsuarioList = new SelectList(db.tipo_usuario.ToList(), "id_tipo_usuario", "tipo");
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,usuario1,clave,celular,tipo_usuario,saldo")] usuario user)
        {
            if (ModelState.IsValid)
            {
                db.usuarios.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoUsuarioList = new SelectList(db.tipo_usuario.ToList(), "id_tipo_usuario", "tipo");
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario user = db.usuarios.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoUsuarioList = new SelectList(db.tipo_usuario.ToList(), "id_tipo_usuario", "tipo", user.tipo_usuario);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,usuario1,clave,celular,tipo_usuario,saldo")] usuario user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoUsuarioList = new SelectList(db.tipo_usuario.ToList(), "id_tipo_usuario", "tipo", user.tipo_usuario);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario user = db.usuarios.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario user = db.usuarios.Find(id);
            db.usuarios.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
