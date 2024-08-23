namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class detalle_carrito
    {
        [Key]
        public int id_detalle_carrito { get; set; }

        public int? id_carrito { get; set; }

        public int? id_producto { get; set; }

        public int cantidad { get; set; }

        public virtual carrito carrito { get; set; }

        public virtual producto producto { get; set; }
    }
}
