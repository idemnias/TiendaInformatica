using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TiendaInformatica.DAL;
using TiendaInformatica.Model;

namespace TiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        UnitofWork unit = new UnitofWork();
        Empleado empleado = new Empleado();
        Proveedor proveedor = new Proveedor();
        Categoria categoria = new Categoria();
        Producto producto = new Producto();
        Cliente cliente = new Cliente();
        LineaVenta lineaventa = new LineaVenta();
        HashSet<LineaVenta> listalineaventas = new HashSet<LineaVenta>();
        Venta venta;
        Cliente clientetpv = new Cliente();
        string user;
        double total;

        string categorianombre;
        string proveedornombre;

        public MainWindow()
        {
            //para conectarme en casa
            //connectionString = "Data Source=(local)\sqlexpress or IDEMNIAS-PC

            
            InitializeComponent();
            OcultarPestañas();
            grid_empleados.DataContext = empleado;
            grid_proveedores.DataContext = proveedor;
            grid_categorias.DataContext = categoria;
            grid_productos.DataContext = producto;
            grid_clientes.DataContext = cliente;
            grid_TPV.DataContext = lineaventa;
            dg_empleados.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
            dg_proveedores.ItemsSource = unit.RepositorioProveedor.ObtenerTodo().ToList();
            dg_categorias.ItemsSource = unit.RepositorioCategoria.ObtenerTodo().ToList();
            dg_productos.ItemsSource = unit.RepositorioProducto.ObtenerTodo().ToList();
            dg_clientes.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
            RellenarCombobox_Categorias();
            RellenarCombobox_Proveedores();
            RellenarCombobox_Clientes();
        }


/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

#region USUARIO
        private void bt_validar_usuario_Click(object sender, RoutedEventArgs e)
        {
            Empleado auxusuario = unit.RepositorioEmpleado.ObtenerTodo().Where(c => c.Usuario.Equals(tb_u_usuario.Text)).FirstOrDefault();
            if (auxusuario!=null)
            {
                if (auxusuario.Contraseña.Equals(pw_usuario.Password.ToString()))
                {
                    user = auxusuario.Nombre;
                    if (auxusuario.TipoCuenta.Equals("Administrador"))
                    {
                        EnseñarTodaslasPestañas();
                    }
                    else
                    {
                        EnseñarPestañatpv();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("La contraseña es incorrecta");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("No hay ese usuario");
            }
            tb_u_usuario.Text = "";
            pw_usuario.Password = null;
        }
        #endregion

#region ACTIVAR DESACTIVAR BOTONES Y LIMPIAR DATOS

        private void OcultarPestañas()
        {
            TabProductos.Visibility = Visibility.Hidden;
            TabProveedores.Visibility = Visibility.Hidden;
            TabClientes.Visibility = Visibility.Hidden;
            TabCategorias.Visibility = Visibility.Hidden;
            TabEmpleados.Visibility = Visibility.Hidden;
            TabTPV.Visibility = Visibility.Hidden;
        }
        private void EnseñarTodaslasPestañas()
        {
            TabProductos.Visibility = Visibility.Visible;
            TabProveedores.Visibility = Visibility.Visible;
            TabClientes.Visibility = Visibility.Visible;
            TabCategorias.Visibility = Visibility.Visible;
            TabEmpleados.Visibility = Visibility.Visible;
            TabTPV.Visibility = Visibility.Visible;
        }
        private void EnseñarPestañatpv()
        {
            TabProductos.Visibility = Visibility.Collapsed;
            TabProveedores.Visibility = Visibility.Collapsed;
            TabClientes.Visibility = Visibility.Collapsed;
            TabCategorias.Visibility = Visibility.Collapsed;
            TabEmpleados.Visibility = Visibility.Collapsed;
            TabTPV.Visibility = Visibility.Visible;
        }

        private void ActivarBotonesEmpleado()
        {
            bt_e_añadir.IsEnabled = false;
            bt_e_modificar.IsEnabled = true;
            bt_e_eliminar.IsEnabled = true;
        }

        private void DesactivarBotonesEmpleado()
        {
            bt_e_modificar.IsEnabled = false;
            bt_e_eliminar.IsEnabled = false;
            bt_e_añadir.IsEnabled = true;
        }
        private void ActivarBotonesProveedores()
        {
            bt_p_añadir.IsEnabled = false;
            bt_p_modificar.IsEnabled = true;
            bt_p_eliminar.IsEnabled = true;
        }

        private void DesactivarBotonesProveedores()
        {
            bt_p_modificar.IsEnabled = false;
            bt_p_eliminar.IsEnabled = false;
            bt_p_añadir.IsEnabled = true;
        }
        private void ActivarBotonesCategorias()
        {
            bt_c_añadir.IsEnabled = false;
            bt_c_modificar.IsEnabled = true;
            bt_c_eliminar.IsEnabled = true;
        }

        private void DesactivarBotonesCategorias()
        {
            bt_c_modificar.IsEnabled = false;
            bt_c_eliminar.IsEnabled = false;
            bt_c_añadir.IsEnabled = true;
        }
        private void ActivarBotonesProductos()
        {
            bt_pr_añadir.IsEnabled = false;
            bt_pr_modificar.IsEnabled = true;
            bt_pr_eliminar.IsEnabled = true;
        }

        private void DesactivarBotonesProductos()
        {
            bt_pr_modificar.IsEnabled = false;
            bt_pr_eliminar.IsEnabled = false;
            bt_pr_añadir.IsEnabled = true;
        }
        private void ActivarBotonesClientes()
        {
            bt_cl_añadir.IsEnabled = false;
            bt_cl_modificar.IsEnabled = true;
            bt_cl_eliminar.IsEnabled = true;
        }
        public void DesactivarBotonesClientes()
        {
            bt_cl_modificar.IsEnabled = false;
            bt_cl_eliminar.IsEnabled = false;
            bt_cl_añadir.IsEnabled = true;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        private void RellenarCombobox_Proveedores()
        {
            cb_pr_proveedorid.Items.Clear();
            List<Proveedor> listaproveedores = unit.RepositorioProveedor.ObtenerTodo().ToList();
            foreach (var item in listaproveedores)
            {
                cb_pr_proveedorid.Items.Add(item.Nombre);
            }
            
        }

        private void RellenarCombobox_Categorias()
        {
            cb_pr_categoriaid.Items.Clear();
            List<Categoria> listacategorias = unit.RepositorioCategoria.ObtenerTodo().ToList();
            foreach (var item in listacategorias)
            {
                cb_pr_categoriaid.Items.Add(item.Nombre);
            }
        }

        private void RellenarCombobox_Clientes()
        {
            cb_clientetpv.Items.Clear();
            List<Cliente> listaclientes = unit.RepositorioCliente.ObtenerTodo().ToList();
            foreach (var item in listaclientes)
            {
                cb_clientetpv.Items.Add(item.Nombre);
            }
            if( cb_clientetpv!=null)cb_clientetpv.SelectedIndex = 0;
        }

        private void LimpiarEmpleados()
        {
            empleado = new Empleado();
            grid_empleados.DataContext = empleado;
        }
        private void LimpiarProveedores()
        {
            proveedor = new Proveedor();
            grid_proveedores.DataContext = proveedor;
        }
        private void LimpiarCategorias()
        {
            categoria = new Categoria();
            grid_categorias.DataContext = categoria;
            tb_c_imagen.Text = "";
            BitmapImage bit = new BitmapImage();
            imagen.Source = bit;
        }
        private void LimpiarProductos()
        {
            producto = new Producto();
            grid_productos.DataContext = producto;
            tb_pr_imagen.Text = "";
            BitmapImage bit2 = new BitmapImage();
            imagen_pr.Source = bit2;
        }
        private void LimpiarClientes()
        {
            cliente = new Cliente();
            grid_clientes.DataContext = cliente;
        }
        #endregion

#region EMPLEADOS
        /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        private void bt_nuevo_Click(object sender, RoutedEventArgs e)
        {   
            DesactivarBotonesEmpleado();
            LimpiarEmpleados();
        }

        private void dg_empleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                empleado = (Empleado)dg_empleados.SelectedItem;
                grid_empleados.DataContext = empleado;
                ActivarBotonesEmpleado();
            }
            catch (Exception)
            {
            }
                
        }

        private void bt_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Crear(empleado);
            dg_empleados.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
            DesactivarBotonesEmpleado();
        }

        private void bt_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Actualizar(empleado);
            dg_empleados.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
        }

        private void bt_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioEmpleado.Eliminar(empleado);
            dg_empleados.ItemsSource = unit.RepositorioEmpleado.ObtenerTodo().ToList();
            DesactivarBotonesEmpleado();
            LimpiarEmpleados();
        }
        #endregion

#region PROVEEDORES
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        private void bt_p_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Crear(proveedor);
            dg_proveedores.ItemsSource = unit.RepositorioProveedor.ObtenerTodo().ToList() ;
            DesactivarBotonesProveedores();
            RellenarCombobox_Proveedores();
        }

        private void bt_p_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Actualizar(proveedor);
            dg_proveedores.ItemsSource = unit.RepositorioProveedor.ObtenerTodo().ToList();
            RellenarCombobox_Proveedores();
        }

        private void bt_p_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProveedor.Eliminar(proveedor);
            dg_proveedores.ItemsSource = unit.RepositorioProveedor.ObtenerTodo();
            DesactivarBotonesProveedores();
            LimpiarProveedores();
            RellenarCombobox_Proveedores();
        }

        private void bt_p_nuevo_Click(object sender, RoutedEventArgs e)
        {
            DesactivarBotonesProveedores();
            LimpiarProveedores();
        }

        private void dg_proveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                proveedor = (Proveedor)dg_proveedores.SelectedItem;
                grid_proveedores.DataContext = proveedor;
                ActivarBotonesProveedores();
            }
            catch (Exception)
            {
            }
            
        }
        #endregion

#region CATEGORIAS
        /*-----------------------------------------------------------------------------------------------------------------------------------------*/
        private void bt_c_elegir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog explorador = new System.Windows.Forms.OpenFileDialog();
            explorador.InitialDirectory = Environment.CurrentDirectory+@"\Imagenes";
            explorador.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            explorador.ShowDialog();
            tb_c_imagen.Text = explorador.FileName.ToString();
            EnseñarImagen(tb_c_imagen.Text);
        }

       

        private void EnseñarImagen(string ruta)
        {
            try
            {
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri(ruta);
                bit.EndInit();
                imagen.Source = bit;
                imagen.Width = 172;
                imagen.Height = 172;
            }
            catch (Exception)
            {
            }
            
        }

        private void bt_c_añadir_Click(object sender, RoutedEventArgs e)
        {
            categoria.Imagen = tb_c_imagen.Text;
            unit.RepositorioCategoria.Crear(categoria);
            dg_categorias.ItemsSource = unit.RepositorioCategoria.ObtenerTodo();
            DesactivarBotonesCategorias();
            RellenarCombobox_Categorias();
        }

        private void bt_c_modificar_Click(object sender, RoutedEventArgs e)
        {
            categoria.Imagen = tb_c_imagen.Text;
            unit.RepositorioCategoria.Actualizar(categoria);
            dg_categorias.ItemsSource = unit.RepositorioCategoria.ObtenerTodo();
            RellenarCombobox_Categorias();
        }

        private void bt_c_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCategoria.Eliminar(categoria);
            dg_categorias.ItemsSource = unit.RepositorioCategoria.ObtenerTodo();
            DesactivarBotonesCategorias();
            LimpiarCategorias();
            RellenarCombobox_Categorias();
        }

        private void bt_c_nuevo_Click(object sender, RoutedEventArgs e)
        {
            DesactivarBotonesCategorias();
            LimpiarCategorias();
        }

        private void dg_categorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                categoria = (Categoria)dg_categorias.SelectedItem;
                tb_c_imagen.Text = categoria.Imagen;
                EnseñarImagen(tb_c_imagen.Text);
                grid_categorias.DataContext = categoria;
                ActivarBotonesCategorias();
            }
            catch (Exception)
            {
            }
            
        }
        #endregion

#region PRODUCTOS
        /*-----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        private void bt_pr_elegir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog explorador1 = new System.Windows.Forms.OpenFileDialog();
            explorador1.InitialDirectory = Environment.CurrentDirectory + @"\Imagenes";
            explorador1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            explorador1.ShowDialog();
            tb_pr_imagen.Text = explorador1.FileName.ToString();
            EnseñarImagenProducto(tb_pr_imagen.Text);
        }
        private void EnseñarImagenProducto(string ruta)
        {
            try
            {
                BitmapImage bit2 = new BitmapImage();
                bit2.BeginInit();
                bit2.UriSource = new Uri(ruta);
                bit2.EndInit();
                imagen_pr.Source = bit2;
                imagen_pr.Width = 113;
                imagen_pr.Height = 113;
            }
            catch (Exception)
            {

            }
            
        }

        private int ObtenerCategoriaId(string categoriastring)
        {
            int id;
            Categoria aux = unit.RepositorioCategoria.ObtenerTodo().ToList().Where(c => c.Nombre.Equals(categoriastring)).FirstOrDefault();
            id = aux.CategoriaId;
            return id;
        }
        private int ObtenerProveedorId(string proveedorstring)
        {
            int id;
            Proveedor aux = unit.RepositorioProveedor.ObtenerTodo().ToList().Where(c => c.Nombre.Equals(proveedorstring)).FirstOrDefault();
            id = aux.ProveedorId;
            return id;
        }

        private void bt_pr_añadir_Click(object sender, RoutedEventArgs e)
        {
            producto.Imagen = tb_pr_imagen.Text;
            producto.CategoriaId = ObtenerCategoriaId(cb_pr_categoriaid.Text);
            producto.ProveedorId = ObtenerProveedorId(cb_pr_proveedorid.Text);
            unit.RepositorioProducto.Crear(producto);
            dg_productos.ItemsSource = unit.RepositorioProducto.ObtenerTodo();
            DesactivarBotonesProductos();
        }

        private void bt_pr_modificar_Click(object sender, RoutedEventArgs e)
        {
            producto.Imagen = tb_pr_imagen.Text;
            producto.ProveedorId = ObtenerProveedorId(cb_pr_proveedorid.Text);
            producto.CategoriaId = ObtenerCategoriaId(cb_pr_categoriaid.Text);
            unit.RepositorioProducto.Actualizar(producto);
            dg_productos.ItemsSource = unit.RepositorioProducto.ObtenerTodo();
        }

        private void bt_pr_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioProducto.Eliminar(producto);
            dg_productos.ItemsSource = unit.RepositorioProducto.ObtenerTodo();
            DesactivarBotonesCategorias();
            LimpiarCategorias();
        }

        private void bt_pr_nuevo_Click(object sender, RoutedEventArgs e)
        {
            DesactivarBotonesProductos();
            LimpiarProductos();
        }

        private void dg_productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                producto = (Producto)dg_productos.SelectedItem;
                tb_pr_imagen.Text = producto.Imagen;
                if (tb_pr_imagen.Text != "") { EnseñarImagenProducto(tb_pr_imagen.Text); }
                grid_productos.DataContext = producto;
                Categoria aux = unit.RepositorioCategoria.ObtenerTodo().ToList().Where(c => c.CategoriaId.Equals(producto.CategoriaId)).FirstOrDefault();
                Proveedor auxp = unit.RepositorioProveedor.ObtenerTodo().ToList().Where(c => c.ProveedorId.Equals(producto.ProveedorId)).FirstOrDefault();
                cb_pr_categoriaid.SelectedValue = aux.Nombre;
                cb_pr_proveedorid.SelectedValue = auxp.Nombre;
                ActivarBotonesProductos();
            }
            catch (Exception)
            { 
            }
            
        }
        private void cb_pr_proveedorid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            proveedornombre = cb_pr_proveedorid.Text;
        }

        private void cb_pr_categoriaid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categorianombre = cb_pr_categoriaid.Text;
        }

        #endregion

#region CLIENTES
        private void bt_cl_añadir_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Crear(cliente);
            dg_clientes.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
            DesactivarBotonesClientes();
        }

        private void bt_cl_modificar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Actualizar(cliente);
            dg_clientes.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
        }

        private void bt_cl_eliminar_Click(object sender, RoutedEventArgs e)
        {
            unit.RepositorioCliente.Eliminar(cliente);
            dg_clientes.ItemsSource = unit.RepositorioCliente.ObtenerTodo().ToList();
            DesactivarBotonesClientes();
            LimpiarClientes();
        }

        private void bt_cl_nuevo_Click(object sender, RoutedEventArgs e)
        {
            DesactivarBotonesClientes();
            LimpiarClientes();
        }

        private void dg_clientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                try
                {
                    cliente = (Cliente)dg_clientes.SelectedItem;
                    grid_clientes.DataContext = cliente;
                    ActivarBotonesClientes();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
            }
            
        }

        #endregion

#region TAB
        private void ControlTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ControlTab.SelectedIndex==6)
            {
                GenerarBotones();
            }
            else if (ControlTab.SelectedIndex == 4)
            {
                //Problemas si relleno aqui los combobox
                //RellenarCombobox_Categorias();
                //RellenarCombobox_Proveedores();
            }
            else
            {

            }
        }
#endregion

#region METODOS TPV Y TABS
        private Image EnseñarImagenTPV(string ruta)
        {
            try
            {
                Image imagen = new Image();
                BitmapImage bit3 = new BitmapImage();
                bit3.BeginInit();
                bit3.UriSource = new Uri(ruta);
                bit3.EndInit();
                imagen.Source = bit3;
                imagen.Width = 100;
                imagen.Height = 100;
                return imagen;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public void GenerarBotones()
        {
            try

            {

                while (this.sp_categorias.Children.Count > 0)
                {
                    this.sp_categorias.Children.RemoveAt(this.sp_categorias.Children.Count - 1);
                }
                List<Categoria> listcategorias = new List<Categoria>();
                listcategorias = unit.RepositorioCategoria.ObtenerTodo();
                for (int i = 0; i < listcategorias.Count; i++)
                {
                    Button n = new Button();
                    n.Content = listcategorias[i].Nombre;
                    n.Height = 40;
                    n.Width = 130;
                    n.Background = Brushes.LightBlue;
                    n.Margin = new Thickness(2);
                    n.Click += categoria_click;
                    this.sp_categorias.Children.Add(n);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void categoria_click(object sender, RoutedEventArgs e)
        {

            List<Producto> listproductos = new List<Producto>();
            Categoria cat = new Categoria();



            this.sp_productos.Children.Clear();
            var aux = e.OriginalSource;
            if (aux.GetType() == typeof(Button))
            {
                var a = sender as Button;
                cat = unit.RepositorioCategoria.ObtenerUno(d => d.Nombre.Equals(a.Content.ToString()));
                listproductos = unit.RepositorioProducto.ObtenerVarios(c => c.CategoriaId.Equals(cat.CategoriaId));

                for (int i = 0; i < listproductos.Count; i++)
                {
                    StackPanel stackp = new StackPanel();
                    stackp.VerticalAlignment = VerticalAlignment.Center;
                    stackp.HorizontalAlignment = HorizontalAlignment.Stretch;
                    stackp.Orientation = Orientation.Vertical;
                    stackp.Width = 100;
                    stackp.Height = 125;

                    Label l = new Label();
                    l.Content = listproductos[i].Nombre;
                    l.HorizontalAlignment = HorizontalAlignment.Center;
                    l.FontSize = 10;
                    
                    stackp.Children.Add(EnseñarImagenTPV(listproductos[i].Imagen));
                    stackp.Children.Add(l);

                    Button b = new Button();
                    b.Width = 110;
                    b.Height = 125;
                    b.Margin = new Thickness(2);
                    b.Content = stackp;
                    b.Name = "P_" + listproductos[i].ProductoId;
                    b.Click += producto_click;
                    if (listproductos[i].Stock > 0) 
                    {
                        b.Background = Brushes.LightBlue;
                    }
                    else
                    {
                        b.Background = Brushes.LightSalmon;
                    }

                    this.sp_productos.Children.Add(b);
                }
            }
        }
        private void producto_click(object sender, RoutedEventArgs e)
        {

            var aux = e.OriginalSource;
            if (aux.GetType() == typeof(Button))
            {
                Button b = (Button)aux;

                String[] btname = b.Name.Split('_');
                int cx = Convert.ToInt32(btname[1].Trim());
                    Producto productoTPV = unit.RepositorioProducto.ObtenerUno(c=>c.ProductoId == cx);
                //setProductoTPV(productoTPV);

                if (productoTPV.Stock < 1)
                {
                    MessageBox.Show("El producto no tiene stock", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                    if (venta == null)
                    {
                        venta = new Venta();
                        Empleado usuario = unit.RepositorioEmpleado.ObtenerUno(c => c.Nombre.Equals(user));
                        venta.EmpleadoId = usuario.UsuarioId;
                        clientetpv = unit.RepositorioCliente.ObtenerUno(c => c.Nombre.Equals(cb_clientetpv.Text));
                        venta.ClienteId = Convert.ToInt32(clientetpv.ClienteId);
                        venta.Fecha = DateTime.Now;
                    }
                    if (venta.LineaVentas.Where(c => c.Productos.ProductoId.Equals(productoTPV.ProductoId)).FirstOrDefault() == null)
                    {
                        // la venta no tiene actualmente ese producto, se añade como linea
                        lineaventa = new LineaVenta();
                        lineaventa.ProductoId = productoTPV.ProductoId;
                        lineaventa.Cantidad = 1;
                        lineaventa.VentaId = venta.VentaId;
                        lineaventa.Productos = productoTPV;
                        lineaventa.Ventas = venta;

                        venta.LineaVentas.Add(lineaventa);

                        total += lineaventa.Productos.Precio;

                        productoTPV.Stock--;
                        unit.RepositorioProducto.Actualizar(productoTPV);
                        listalineaventas.Add(lineaventa);
                    }
                    else
                    {
                        // la venta ya contiene ese producto, se incrementa la cantidad en la linea
                        lineaventa = venta.LineaVentas.Where(c => c.Productos.ProductoId.Equals(productoTPV.ProductoId)).FirstOrDefault();
                        total += lineaventa.Productos.Precio;
                        setTotal(lineaventa);
                        productoTPV.Stock--;
                        unit.RepositorioProducto.Actualizar(productoTPV);
                    }
                    dg_TPV.ItemsSource = "";
                    //dg_TPV.ItemsSource = venta.LineaVentas.ToList();
                    dg_TPV.ItemsSource = listalineaventas;
                }
            }
        }

        private void setTotal(LineaVenta linea)
        {
            foreach (var item in listalineaventas)
            {
                if (item.ProductoId==linea.ProductoId)
                {
                    item.Cantidad++;
                }
            }
        }
        #endregion

        private void cb_clientetpv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientetpv = unit.RepositorioCliente.ObtenerUno(c => c.Nombre.Equals(cb_clientetpv.Text));
            venta.ClienteId = Convert.ToInt32(cliente.ClienteId);
        }
    }


}


