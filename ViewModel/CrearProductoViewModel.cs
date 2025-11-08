using System.ComponentModel.DataAnnotations;
using tl2_tp8_2025_carlitos0707.Models;

namespace tl2_tp8_2025_carlitos0707.ViewModel;


public class CrearProductoViewModel
{
    [StringLength(100)]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Ingrese el precio")]
    public int Precio { get; set; }


    public CrearProductoViewModel()
    {

    }
    
    public CrearProductoViewModel(Producto producto)
    {
        Descripcion = producto.Descripcion;
        Precio = producto.Precio;
    }

}