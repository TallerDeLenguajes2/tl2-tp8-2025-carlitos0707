namespace Presupuestos;

using PresupuestosDetalles;

public class Presupuesto
{
    public int IdPresupuesto { get; set; }
    public string NombreDestinatario { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<DetallePresupuesto> Detalles { get; set; }

    public double MontoPresupuesto()
    {
        double monto = 0;

        foreach (DetallePresupuesto detalle in Detalles)
        {
            monto += detalle.Total();
        }
        return monto;
    }
    public double MontoPresupuestoIVA()
    {
        return MontoPresupuesto() * (1 + 0.21);
    }
    

    public int ContadorProductos()
    {
        int cant = 0;
        foreach (DetallePresupuesto detalle in Detalles)
        {
            cant += detalle.Cantidad;
        }

        return cant;
    }

}