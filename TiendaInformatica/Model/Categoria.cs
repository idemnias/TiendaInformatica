using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaInformatica.Model
{
    [Table("Categorias")]
    public class Categoria: PropertyValidateModel
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Producto>Productos { get; set; }
    }
}
