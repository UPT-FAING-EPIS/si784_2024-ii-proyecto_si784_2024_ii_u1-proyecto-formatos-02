namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class calificacion_producto
    {
        [Key]
        public int id_calificacion { get; set; }

        public int? id_producto { get; set; }

        public int? id_usuario { get; set; }

        public byte? calificacion { get; set; }

        [Column(TypeName = "text")]
        public string comentario { get; set; }

        public DateTime? fecha_calificacion { get; set; }

        public virtual producto producto { get; set; }
    }
}
