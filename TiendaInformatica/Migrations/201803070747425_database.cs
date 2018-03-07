namespace TiendaInformatica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Imagen = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Double(nullable: false),
                        Descripcion = c.String(),
                        Stock = c.Int(nullable: false),
                        Imagen = c.String(),
                        ProveedorId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.ProveedorId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.LineaVentas",
                c => new
                    {
                        LineaVentaId = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        VentaId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        PrecioTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LineaVentaId)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        Empleados_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Empleados", t => t.Empleados_UsuarioId)
                .Index(t => t.ClienteId)
                .Index(t => t.Empleados_UsuarioId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Direccion = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        Contraseña = c.String(),
                        Nombre = c.String(),
                        TipoCuenta = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Direccion = c.String(),
                        TlfContacto = c.String(),
                        Contacto = c.String(),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.LineaVentas", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Ventas", "Empleados_UsuarioId", "dbo.Empleados");
            DropForeignKey("dbo.Ventas", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.LineaVentas", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Productos", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Ventas", new[] { "Empleados_UsuarioId" });
            DropIndex("dbo.Ventas", new[] { "ClienteId" });
            DropIndex("dbo.LineaVentas", new[] { "ProductoId" });
            DropIndex("dbo.LineaVentas", new[] { "VentaId" });
            DropIndex("dbo.Productos", new[] { "CategoriaId" });
            DropIndex("dbo.Productos", new[] { "ProveedorId" });
            DropTable("dbo.Proveedores");
            DropTable("dbo.Empleados");
            DropTable("dbo.Clientes");
            DropTable("dbo.Ventas");
            DropTable("dbo.LineaVentas");
            DropTable("dbo.Productos");
            DropTable("dbo.Categorias");
        }
    }
}
