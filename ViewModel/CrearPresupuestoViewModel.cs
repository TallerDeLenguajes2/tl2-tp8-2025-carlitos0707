using System.ComponentModel.DataAnnotations;

namespace tl2_tp8_2025_carlitos0707.ViewModel;


public class CrearPresupuestoViewModel
{
    [Required(ErrorMessage = "Ingrese el nombre")]
    public string NombreDestinatario { get; set; }
    [Required(ErrorMessage = "Ingrese una fecha")]
    public DateTime FechaCreacion { get; set; }
    public string MensajeError {get; set; }

    
}