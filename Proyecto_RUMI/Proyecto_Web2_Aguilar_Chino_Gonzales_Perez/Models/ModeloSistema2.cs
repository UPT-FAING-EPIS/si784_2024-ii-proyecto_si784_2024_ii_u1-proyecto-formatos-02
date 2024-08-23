using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
	public partial class ModeloSistema2 : DbContext
	{
		public ModeloSistema2()
			: base("name=ModeloSistema2")
		{
		}

		public virtual DbSet<calificacion_producto> calificacion_producto { get; set; }
		public virtual DbSet<carrito> carrito { get; set; }
		public virtual DbSet<Compra> Compra { get; set; }
		public virtual DbSet<detalle_carrito> detalle_carrito { get; set; }
		public virtual DbSet<DetalleCompra> DetalleCompra { get; set; }
		public virtual DbSet<favoritos> favoritos { get; set; }
		public virtual DbSet<imagen_producto> imagen_producto { get; set; }
		public virtual DbSet<producto> producto { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<calificacion_producto>()
				.Property(e => e.comentario)
				.IsUnicode(false);

			modelBuilder.Entity<Compra>()
				.Property(e => e.total)
				.HasPrecision(10, 2);

			modelBuilder.Entity<DetalleCompra>()
				.Property(e => e.precio)
				.HasPrecision(10, 2);

			modelBuilder.Entity<imagen_producto>()
				.Property(e => e.url_imagen)
				.IsUnicode(false);

			modelBuilder.Entity<imagen_producto>()
				.Property(e => e.descripcion)
				.IsUnicode(false);

			modelBuilder.Entity<producto>()
				.Property(e => e.nombre)
				.IsUnicode(false);

			modelBuilder.Entity<producto>()
				.Property(e => e.descripcion)
				.IsUnicode(false);

			modelBuilder.Entity<producto>()
				.Property(e => e.precio)
				.HasPrecision(10, 2);

			modelBuilder.Entity<producto>()
				.Property(e => e.categoria)
				.IsUnicode(false);
		}
	}
}
