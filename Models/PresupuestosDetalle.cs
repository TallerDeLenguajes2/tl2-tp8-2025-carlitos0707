namespace tl2_tp8_2025_carlitos0707.Models;

public class DetallePresupuesto
{
    public int Cantidad { get; set; }
    public Producto producto { get; set; }

    public double Total()
    {
        return producto.Precio * Cantidad;
    }
}