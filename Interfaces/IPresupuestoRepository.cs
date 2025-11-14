namespace tl2_tp8_2025_carlitos0707.Interfaces;
using tl2_tp8_2025_carlitos0707.Models;
using tl2_tp8_2025_carlitos0707.ViewModel;
public interface IPresupuestoRepository
{
    public void AgregarProducto(int IdPresupuesto, int IdProducto, int cantidad);
    public void UpdateProducto(int IdPresupuesto,int IdProducto,int cantidad);
    public void BorrarProducto(DetallesPresupuestoViewModel borrarProductoViewModel);
    public DetallePresupuesto GetProducto(int idPresupuesto,int idProducto);
    public void CrearPresupuesto(Presupuesto presupuesto);
    public List<DetallePresupuesto> GetDetalles(int id);
    public void Update(Presupuesto presupuesto);
    public void EliminarDetalles(int id);
    public void Eliminar(int id);
    public List<Presupuesto> GetAll();
    public Presupuesto ObtenerPorID(int id);
}