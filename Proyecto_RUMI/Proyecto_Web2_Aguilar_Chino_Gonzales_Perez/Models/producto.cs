namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity;
	using System.Data.Entity.Spatial;
	using System.Linq;

	[Table("producto")]
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            calificacion_producto = new HashSet<calificacion_producto>();
            detalle_carrito = new HashSet<detalle_carrito>();
            DetalleCompra = new HashSet<DetalleCompra>();
            favoritos = new HashSet<favoritos>();
            imagen_producto = new HashSet<imagen_producto>();
        }

        [Key]
        public int id_producto { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        public decimal precio { get; set; }

        public int stock { get; set; }

        [StringLength(50)]
        public string categoria { get; set; }

        public DateTime? fecha_creacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<calificacion_producto> calificacion_producto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_carrito> detalle_carrito { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleCompra> DetalleCompra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<favoritos> favoritos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<imagen_producto> imagen_producto { get; set; }


		public List<producto> Listar()
		{
			var query = new List<producto>();
			try
			{
				using (var db = new ModeloSistema())// cambiar
				{
					query = db.producto.Include("imagen_producto").ToList();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}

		public producto Obtener(int id)
		{
			var query = new producto();

			try
			{
				using (var db = new ModeloSistema()) // cambiar// cambiar
				{
					query = db.producto.Include("imagen_producto")
						.Where(x => x.id_producto == id)
						.SingleOrDefault();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}

		public List<producto> Buscar(string buscar)
		{
			var query = new List<producto>();
			try
			{
				using (var db = new ModeloSistema()) // cambiar
				{
					query = db.producto.Include("imagen_producto")
						.Where(x => x.nombre.Contains(buscar) || x.descripcion.Contains(buscar))
						.ToList();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}

		public void Guardar()
		{
			try
			{
				using (var db = new ModeloSistema())// cambiar
				{
					if (this.id_producto > 0)
					{
						db.Entry(this).State = EntityState.Modified;
					}
					else
					{
						db.Entry(this).State = EntityState.Added;
					}
					db.SaveChanges();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Eliminar()
		{
			try
			{
				using (var db = new ModeloSistema()) // cambiar
				{
					db.Entry(this).State = EntityState.Deleted;
					db.SaveChanges();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}


	}
}
