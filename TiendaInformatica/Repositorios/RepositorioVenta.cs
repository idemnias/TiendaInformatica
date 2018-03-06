using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaInformatica.DAL;
using TiendaInformatica.Model;

namespace TiendaInformatica.Repositorios
{
    public class RepositorioVenta : RepositorioGenerico<Venta>
    {
        public RepositorioVenta(TiendaInformaticaContext context) : base(context)
        {
        }
    }
}
