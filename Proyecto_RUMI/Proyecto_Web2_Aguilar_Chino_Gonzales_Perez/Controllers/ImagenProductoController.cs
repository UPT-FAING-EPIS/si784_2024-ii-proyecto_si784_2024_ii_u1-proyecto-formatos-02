using Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Controllers
{
	public class ImagenProductoController : Controller
	{
		private ModeloSistema db = new ModeloSistema();
		public ActionResult Index(int idProducto)
		{
			var imagenes = db.imagen_producto.Where(i => i.id_producto == idProducto).ToList();
			ViewBag.idProducto = idProducto;
			return View(imagenes);
		}

		// GET: ImagenProducto/Create/5
		public ActionResult Create(int idProducto)
		{
			ViewBag.idProducto = idProducto;
			var imagen = new imagen_producto();
			imagen.id_producto = idProducto;
			return View(imagen);
		}

		// POST: ImagenProducto/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "id_imagen,id_producto,url_imagen,descripcion,es_principal,fecha_subida")] imagen_producto imagen)
		{
			if (ModelState.IsValid)
			{
				db.imagen_producto.Add(imagen);
				db.SaveChanges();
				return RedirectToAction("Index", new { idProducto = imagen.id_producto });
			}
			return View(imagen);
		}

		// GET: ImagenProducto/Edit/5
		public ActionResult Edit(int id)
		{
			var imagen = db.imagen_producto.Find(id);
			if (imagen == null)
			{
				return HttpNotFound();
			}
			return View(imagen);
		}

		// POST: ImagenProducto/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "id_imagen,id_producto,url_imagen,descripcion,es_principal,fecha_subida")] imagen_producto imagen)
		{
			if (ModelState.IsValid)
			{
				db.Entry(imagen).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index", new { idProducto = imagen.id_producto });
			}
			return View(imagen);
		}

		// GET: ImagenProducto/Delete/5
		public ActionResult Delete(int id)
		{
			var imagen = db.imagen_producto.Find(id);
			if (imagen == null)
			{
				return HttpNotFound();
			}
			return View(imagen);
		}

		// POST: ImagenProducto/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var imagen = db.imagen_producto.Find(id);
			db.imagen_producto.Remove(imagen);
			db.SaveChanges();
			return RedirectToAction("Index", new { idProducto = imagen.id_producto });
		}
	}
}