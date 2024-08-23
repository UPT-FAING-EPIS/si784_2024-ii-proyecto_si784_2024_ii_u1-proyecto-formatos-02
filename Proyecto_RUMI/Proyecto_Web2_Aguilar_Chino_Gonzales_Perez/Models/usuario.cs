namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("usuario")]
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            transferencias = new HashSet<transferencia>();
            transferencias1 = new HashSet<transferencia>();
        }

        [Key]
        public int id_usuario { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(100)]
        public string usuario1 { get; set; }

        [StringLength(50)]
        public string clave { get; set; }

        [StringLength(9)]
        public string celular { get; set; }
       

        public int? tipo_usuario { get; set; }


        public decimal? saldo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]

        public virtual tipo_usuario tipo_usuario1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transferencia> transferencias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transferencia> transferencias1 { get; set; }



		public List<usuario> Listar()
		{
			var query = new List<usuario>();
			try
			{
				using (var db = new ModeloSistema())
				{
					query = db.usuarios.Include("tipo_usuario1").ToList();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}

		public usuario Obtener(int id)
		{
			var query = new usuario();

			try
			{
				using (var db = new ModeloSistema())
				{
					query = db.usuarios.Include("tipo_usuario1")
						.Where(x => x.id_usuario == id)
						.SingleOrDefault();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}

		public List<usuario> Buscar(string buscar)
		{
			var query = new List<usuario>();
			try
			{
				using (var db = new ModeloSistema())
				{
					query = db.usuarios.Include("tipo_usuario1")
						.Where(x => x.usuario1.Contains(buscar) || x.celular.Contains(buscar))
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
				using (var db = new ModeloSistema())
				{
					if (this.id_usuario > 0)
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
				using (var db = new ModeloSistema())
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
