using tl2_tp8_2025_carlitos0707.ViewModel;

namespace tl2_tp8_2025_carlitos0707.Models;


public class Presupuesto
{
    public int IdPresupuesto { get; set; }
    public string NombreDestinatario { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<DetallePresupuesto> Detalles { get; set; }

    public Presupuesto()
    {
        
    }

    public Presupuesto(PresupuestoViewModel presupuestoViewModel)
    {
        IdPresupuesto = presupuestoViewModel.IdPresupuesto;
        NombreDestinatario = presupuestoViewModel.NombreDestinatario;
        FechaCreacion = presupuestoViewModel.FechaCreacion;
        Detalles = presupuestoViewModel.Detalles;
    }

    public Presupuesto(CrearPresupuestoViewModel crearPresupuestoViewModel)
    {
        NombreDestinatario = crearPresupuestoViewModel.NombreDestinatario;
        FechaCreacion = crearPresupuestoViewModel.FechaCreacion;
    }
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