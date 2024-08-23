using Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Controllers
{
	public class ProductoController : Controller
	{
		// GET: Producto
		private ModeloSistema db = new ModeloSistema();

		// GET: Productos
		public ActionResult Index()
		{
			int userID = Convert.ToInt32(Session["UserID"]);
			string username = Session["Username"] as string;
			string usertype = Session["Usertype"] as string;
			return View(db.producto.ToList());
		}

		// GET: Producto/Details/5
		public ActionResult Details(int id)
		{
			var producto = db.producto.Include("imagen_producto").FirstOrDefault(p => p.id_producto == id);
			if (producto == null)
			{
				return HttpNotFound();
			}
			return View(producto);
		}

		// GET: Producto/Create
		public ActionResult Create()
		{
			var nuevoProducto = new producto();
			return View(nuevoProducto);
		}

		// POST: Producto/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(producto producto)
		{
			if (ModelState.IsValid)
			{
				db.producto.Add(producto);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(producto);
		}

		// GET: Producto/Edit/5
		public ActionResult Edit(int id)
		{
			var producto = db.producto.Find(id);
			if (producto == null)
			{
				return HttpNotFound();
			}
			return View(producto);
		}

		// POST: Producto/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(producto producto)
		{
			if (ModelState.IsValid)
			{
				db.Entry(producto).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(producto);
		}

		// GET: Producto/Delete/5
		public ActionResult Delete(int id)
		{
			var producto = db.producto.Find(id);
			if (producto == null)
			{
				return HttpNotFound();
			}
			return View(producto);
		}

		// POST: Producto/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var producto = db.producto.Find(id);
			db.producto.Remove(producto);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}