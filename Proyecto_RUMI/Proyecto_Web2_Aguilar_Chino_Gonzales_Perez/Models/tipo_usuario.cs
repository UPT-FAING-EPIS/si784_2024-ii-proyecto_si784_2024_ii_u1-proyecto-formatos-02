namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tipo_usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipo_usuario()
        {
            usuarios = new HashSet<usuario>();
        }

        [Key]
        public int id_tipo_usuario { get; set; }

        [StringLength(50)]
        public string tipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuario> usuarios { get; set; }


		public List<tipo_usuario> Listar()
		{
			var query = new List<tipo_usuario>();
			try
			{
				using (var db = new ModeloSistema())
				{
					query = db.tipo_usuario.ToList();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}
		public tipo_usuario Obtener(int id)
		{
			var query = new tipo_usuario();

			try
			{
				using (var db = new ModeloSistema())
				{
					query = db.tipo_usuario
						.Where(x => x.id_tipo_usuario == id)
						.SingleOrDefault();
				}

			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}
		// Buscar
		public List<tipo_usuario> Buscar(string buscar)
		{
			var query = new List<tipo_usuario>();
			try
			{
				using (var db = new ModeloSistema())
				{
					query = db.tipo_usuario
						.Where(x => x.tipo.Contains(buscar)).ToList();
				}
			}
			catch (Exception)
			{
				throw;
			}
			return query;
		}
		//Guardar
		public void Guardar()
		{
			try
			{
				using (var db = new ModeloSistema())
				{
					if (this.id_tipo_usuario > 0)
					{
						db.Entry(this).State = EntityState.Modified;
					}
					else
					{
						db.Entry(this).State = EntityState.Added;
						db.SaveChanges();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		//Eliminar
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
