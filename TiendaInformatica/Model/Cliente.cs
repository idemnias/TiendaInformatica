using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaInformatica.Model
{
    [Table("Clientes")]
    public class Cliente
    {
        public Cliente()
        {
            Ventas = new HashSet<Venta>(); ;
        }

        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
