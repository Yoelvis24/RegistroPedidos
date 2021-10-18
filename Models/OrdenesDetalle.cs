using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPedidos.Models
{
    public class OrdenesDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int OrdenId { get; set; }
        public Ordenes Orden { get; set; }
        public int ProductoId { get; set; }
        public Productos Producto { get; set; }
        public float Cantidad { get; set; }
        public float Costo { get; set; }

        public OrdenesDetalle()
        {
            DetalleId = 0;
            OrdenId = 0;
            ProductoId = 0;
            Cantidad = 0;
            Costo = 0;
        }
    }
}
