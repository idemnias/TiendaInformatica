using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaInformatica.Model
{
    [Table("Empleados")]
    public class Empleado: PropertyValidateModel
    {
        public Empleado()
        {
            Ventas = new HashSet<Venta>();
        }
        [Key]
        [Required]
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string TipoCuenta { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }

    }
}
