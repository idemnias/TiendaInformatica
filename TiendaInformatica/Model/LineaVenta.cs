using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaInformatica.DAL;

namespace TiendaInformatica.Model
{
    [Table("LineaVentas")]
    public class LineaVenta: PropertyValidateModel
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
            UnitofWork unit = new UnitofWork();
            Producto aux = unit.RepositorioProducto.ObtenerUno(c => c.ProductoId == ProductoId);
            PrecioTotal = Cantidad * aux.Precio;
        }


    }
}
