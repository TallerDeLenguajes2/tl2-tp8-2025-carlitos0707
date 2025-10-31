namespace PresupuestosDetalles;

using Productos;
public class DetallePresupuesto
{
    public int Cantidad { get; set; }
    public Producto producto { get; set; }

    public double Total()
    {
        return producto.Precio * Cantidad;
    }
}