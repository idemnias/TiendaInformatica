using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaInformatica.Model
{
    [Table("Ventas")]
    public class Venta: PropertyValidateModel
    {
        public Venta()
        {
            LineaVentas = new HashSet<LineaVenta>();
        }
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }
        public virtual Empleado Empleados { get; set; }
        public virtual Cliente Clientes { get; set; }
        public virtual ICollection<LineaVenta> LineaVentas  { get; set; }
    }
}
