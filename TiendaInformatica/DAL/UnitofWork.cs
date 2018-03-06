using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaInformatica.Repositorios;

namespace TiendaInformatica.DAL
{
    public class UnitofWork
    {
        private TiendaInformaticaContext context = new TiendaInformaticaContext();
        private RepositorioCliente cliente;
        private RepositorioCategoria categoria;
        private RepositorioEmpleado empleado;
        private RepositorioLineadeVenta lineadeVenta;
        private RepositorioProducto producto;
        private RepositorioProveedor proveedor;
        private RepositorioVenta venta;
        public RepositorioCliente RepositorioCliente
        {
            get
            {
                if (this.cliente == null)
                {
                    this.cliente = new RepositorioCliente(context);
                }
                return cliente;
            }
        }
        public RepositorioCategoria RepositorioCategoria
        {
            get
            {
                if (this.categoria == null)
                {
                    this.categoria = new RepositorioCategoria(context);
                }
                return categoria;
            }
        }
        public RepositorioEmpleado RepositorioEmpleado
        {
            get
            {
                if (this.empleado == null)
                {
                    this.empleado = new RepositorioEmpleado(context);
                }
                return empleado;
            }
        }
        public RepositorioLineadeVenta RepositorioLineadeVenta
        {
            get
            {
                if (this.lineadeVenta == null)
                {
                    this.lineadeVenta = new RepositorioLineadeVenta(context);
                }
                return lineadeVenta;
            }
        }
        public RepositorioProducto RepositorioProducto
        {
            get
            {
                if (this.producto == null)
                {
                    this.producto = new RepositorioProducto(context);
                }
                return producto;
            }
        }
        public RepositorioProveedor RepositorioProveedor
        {
            get
            {
                if (this.proveedor == null)
                {
                    this.proveedor = new RepositorioProveedor(context);
                }
                return proveedor;
            }
        }
        public RepositorioVenta RepositorioVenta
        {
            get
            {
                if (this.venta == null)
                {
                    this.venta = new RepositorioVenta(context);
                }
                return venta;
            }
        }
    }
}
