using tl2_tp8_2025_carlitos0707.ViewModel;

namespace tl2_tp8_2025_carlitos0707.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string Descripcion { get; set; }
    public int Precio { get; set; }


    public Producto()
    {
        
    }
    public Producto(ProductoViewModel productoViewModel)
    {
        IdProducto = productoViewModel.IdProducto;
        Descripcion = productoViewModel.Descripcion;
        Precio = productoViewModel.Precio;
    }


    public Producto(CrearProductoViewModel crearProductoViewModel)
    {
        Descripcion = crearProductoViewModel.Descripcion;
        Precio = crearProductoViewModel.Precio;
    }

    
}