namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class total_moneda
    {
        [Key]
        public int id_moneda { get; set; }

        [Column("total_moneda")]
        public decimal? total_moneda1 { get; set; }

        public DateTime? fecha_actualizacion { get; set; }
    }
}
