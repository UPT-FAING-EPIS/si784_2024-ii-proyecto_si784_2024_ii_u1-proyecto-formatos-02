namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class imagen_producto
    {
        [Key]
        public int id_imagen { get; set; }

        public int? id_producto { get; set; }

        [Required]
        [StringLength(255)]
        public string url_imagen { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        //public bool? es_principal { get; set; }
		public bool es_principal { get; set; } = false;

		public DateTime? fecha_subida { get; set; }

        public virtual producto producto { get; set; }
    }
}
