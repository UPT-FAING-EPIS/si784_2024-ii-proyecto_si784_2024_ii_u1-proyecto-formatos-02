namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class favoritos
    {
        [Key]
        public int id_favorito { get; set; }

        public int? id_usuario { get; set; }

        public int? id_producto { get; set; }

        public DateTime? fecha_agregado { get; set; }

        public virtual producto producto { get; set; }
    }
}
