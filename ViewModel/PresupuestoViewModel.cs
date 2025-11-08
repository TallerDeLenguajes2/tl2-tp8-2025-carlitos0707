using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_carlitos0707.Models;
namespace tl2_tp8_2025_carlitos0707.ViewModel
{
    public class PresupuestoViewModel
    {
        public int IdPresupuesto { get; set; }
        [Required]
        public string NombreDestinatario { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        public List<DetallePresupuesto> Detalles { get; set; }

        public PresupuestoViewModel()
        {
            
        }

        public PresupuestoViewModel(Presupuesto presupuesto)
        {
            IdPresupuesto = presupuesto.IdPresupuesto;
            NombreDestinatario = presupuesto.NombreDestinatario;
            FechaCreacion = presupuesto.FechaCreacion;
            Detalles = presupuesto.Detalles;
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
}