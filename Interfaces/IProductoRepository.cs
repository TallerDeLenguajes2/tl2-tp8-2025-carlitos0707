using tl2_tp8_2025_carlitos0707.Models;
namespace tl2_tp8_2025_carlitos0707.Interfaces;


public interface IProductoRepository
{
    public void Delete(int id);
    public List<Producto> GetAll();
    public Producto GetById(int id);
    public void Insertar(Producto producto);
    public void Update(Producto producto);
}