using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaInformatica.Model
{
    [Table("LineaVentas")]
    public class LineaVenta
    {
        public int LineaVentaId { get; set; }
        public int Cantidad { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public double PrecioTotal { get; set; }


        public virtual Venta Ventas { get; set; }
        public virtual Producto Productos { get; set; }

        public void CalcularPrecio()
        {
            PrecioTotal = Cantidad * Productos.Precio;
        }


    }
}
