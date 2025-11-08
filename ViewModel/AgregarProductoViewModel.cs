using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using tl2_tp8_2025_carlitos0707.Models;
namespace tl2_tp8_2025_carlitos0707.ViewModel
{
    public class AgregarProductoViewModel
    {
        public int IdPresupuesto { get; set; }
        public int IdProducto { get; set; }
        [Required][Range(1,int.MaxValue)]
        public int Cantidad { get; set; }
        public SelectList productos;

        public AgregarProductoViewModel()
        {
            
        }
    }
}