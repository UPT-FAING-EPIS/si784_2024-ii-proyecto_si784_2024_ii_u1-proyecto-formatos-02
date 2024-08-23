namespace Proyecto_Web2_Aguilar_Chino_Gonzales_Perez.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;


    [Table("transferencia")]
    public partial class transferencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Key]
        public int id_transferencia { get; set; }

        public int? id_emisor { get; set; }

        public int? id_receptor { get; set; }

        public decimal? cantidad { get; set; }

        public int? estado { get; set; }

        public DateTime? fecha { get; set; }

        public virtual estado_transferencia estado_transferencia { get; set; }

        public virtual usuario usuario { get; set; }

        public virtual usuario usuario1 { get; set; }



        public class TransferenciaResult
        {
            public string NombreEmisor { get; set; }
            public string NombreReceptor { get; set; }
            public decimal? Cantidad { get; set; }
            public DateTime? Fecha { get; set; }
        }

        public List<TransferenciaResult> Lista6Transferencia(int userId)
        {
            var resultados = new List<TransferenciaResult>();

            try
            {
                using (var db = new ModeloSistema())
                {
                    resultados = db.transferencias
                        .Where(t => t.id_receptor == userId || t.id_emisor == userId)
                        .OrderByDescending(t => t.fecha)
                        .Take(6)
                        .Select(t => new
                        {
                            t.id_emisor,
                            t.id_receptor,
                            t.cantidad,
                            t.fecha,
                            EmisorNombre = db.usuarios.Where(u => u.id_usuario == t.id_emisor).Select(u => u.usuario1).FirstOrDefault(),
                            ReceptorNombre = db.usuarios.Where(u => u.id_usuario == t.id_receptor).Select(u => u.usuario1).FirstOrDefault()
                        })
                        .OrderByDescending(t => t.fecha)
                        .Select(t => new TransferenciaResult
                        {
                            NombreEmisor = t.EmisorNombre,
                            NombreReceptor = t.ReceptorNombre,
                            Cantidad = t.cantidad,
                            Fecha = t.fecha
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las transferencias", ex);
            }

            return resultados;
        }

        public List<TransferenciaResult> ListaTransferencia(int userId)
        {
            var resultados = new List<TransferenciaResult>();

            try
            {
                using (var db = new ModeloSistema())
                {
                    resultados = db.transferencias
                        .Where(t => t.id_receptor == userId || t.id_emisor == userId)
                        .Select(t => new
                        {
                            t.id_emisor,
                            t.id_receptor,
                            t.cantidad,
                            t.fecha,
                            EmisorNombre = db.usuarios.Where(u => u.id_usuario == t.id_emisor).Select(u => u.usuario1).FirstOrDefault(),
                            ReceptorNombre = db.usuarios.Where(u => u.id_usuario == t.id_receptor).Select(u => u.usuario1).FirstOrDefault()
                        })
                        .OrderByDescending(t => t.fecha)
                        .ToList()
                        .Select(t => new TransferenciaResult
                        {
                            NombreEmisor = t.EmisorNombre,
                            NombreReceptor = t.ReceptorNombre,
                            Cantidad = t.cantidad,
                            Fecha = t.fecha
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las transferencias", ex);
            }

            return resultados;
        }

        public transferencia Detalle(int id)
        {
            var query = new transferencia();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.transferencias
                        .Where(x => x.id_receptor == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }
    }
}
