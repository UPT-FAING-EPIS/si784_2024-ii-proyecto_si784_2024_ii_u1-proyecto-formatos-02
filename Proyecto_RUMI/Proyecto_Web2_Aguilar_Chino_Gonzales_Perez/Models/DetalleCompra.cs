namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetalleCompra")]
    public partial class DetalleCompra
    {
        [Key]
        public int id_detalle { get; set; }

        public int? id_compra { get; set; }

        public int? id_producto { get; set; }

        public int? cantidad { get; set; }

        public decimal? precio { get; set; }

        public virtual Compra Compra { get; set; }

        public virtual producto producto { get; set; }
    }
}
