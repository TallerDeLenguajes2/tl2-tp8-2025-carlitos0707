using tl2_tp8_2025_carlitos0707.Models;

namespace tl2_tp8_2025_carlitos0707.ViewModel;


public class DetallesPresupuestoViewModel
{
    public int IdPresupuesto { get; set; }
    public int Cantidad { get; set; }
    public Producto producto { get; set; }

    public DetallesPresupuestoViewModel(DetallePresupuesto detalle)
    {
        Cantidad = detalle.Cantidad;
        producto = detalle.producto;
    }
    public DetallesPresupuestoViewModel()
    {
        
    }
}