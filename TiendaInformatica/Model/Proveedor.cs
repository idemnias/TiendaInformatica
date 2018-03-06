using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaInformatica.Model
{
    [Table("Proveedores")]
    public class Proveedor
    {       
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string TlfContacto { get; set; }
        public string Contacto { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
