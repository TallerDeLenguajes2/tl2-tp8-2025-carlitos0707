using System.ComponentModel.DataAnnotations;
using tl2_tp8_2025_carlitos0707.Models;

namespace tl2_tp8_2025_carlitos0707.ViewModel
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Required]
        [Range(0, 999999)]
        public int Precio { get; set; }


        public ProductoViewModel(Producto producto)
        {
            IdProducto = producto.IdProducto;
            Descripcion = producto.Descripcion;
            Precio = producto.Precio;
        }
        public ProductoViewModel()
        {
            
        }
    }
}