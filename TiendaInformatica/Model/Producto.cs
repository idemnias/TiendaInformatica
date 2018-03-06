using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaInformatica.Model
{
    [Table("Productos")]
    public class Producto
    {
        public Producto()
        {
            LineaVentas = new HashSet<LineaVenta>();
        }

        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public int ProveedorId { get; set; }
        public int CategoriaId { get; set; }
        public virtual Proveedor Proveedores { get; set; }
        public virtual Categoria Categorias { get; set; }
        public virtual ICollection<LineaVenta> LineaVentas { get; set; }
    }
}
