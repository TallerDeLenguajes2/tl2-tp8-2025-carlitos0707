using Microsoft.Data.Sqlite;
using tl2_tp8_2025_carlitos0707.Models;
using SQLitePCL;
using tl2_tp8_2025_carlitos0707.Interfaces;
namespace tl2_tp8_2025_carlitos0707.Repositorios;

/*
public interface IProductoRepository
{
    void Insertar(Producto producto);
    void Update(Producto producto);
    List<Producto> GetAll();
    Producto GetById(int id);
    void Delete(int id);
}
*/


public class ProductoRepository : IProductoRepository
{
    string cadenaConexion = "Data Source=Db/Tienda.db";

    public void Delete(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sqlDetalles = "DELETE FROM PresupuestosDetalle WHERE idProducto = @Id";
        using (var comandoDetalles = new SqliteCommand(sqlDetalles, conexion))
        {
            comandoDetalles.Parameters.Add(new SqliteParameter("@Id", id));
            comandoDetalles.ExecuteNonQuery();
        }
        string sql = "DELETE FROM Productos WHERE idProducto = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        comando.ExecuteNonQuery();
        conexion.Close();
    }

    public List<Producto> GetAll()
    {
        List<Producto> productos = new List<Producto>();
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "SELECT * FROM productos";
        using var comando = new SqliteCommand(sql, conexion);
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            var p = new Producto
            {
                IdProducto = Convert.ToInt32(lector["idProducto"]),
                Descripcion = lector["Descripcion"].ToString(),
                Precio = Convert.ToInt32(lector["Precio"])
            };
            productos.Add(p);
        }
        conexion.Close();
        return productos;
    }

    public Producto GetById(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "SELECT * FROM Productos WHERE idProducto = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        using var lector = comando.ExecuteReader();
        if (!lector.Read())
        {
            return null;
        }
        Producto producto = new Producto
        {
            IdProducto = Convert.ToInt32(lector["idProducto"]),
            Descripcion = lector["Descripcion"].ToString(),
            Precio = Convert.ToInt32(lector["Precio"])
        };
        conexion.Close();
        return producto;   
    }

    public void Insertar(Producto producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "SELECT MAX(idProducto) as id FROM Productos";
        using var select = new SqliteCommand(sql, conexion);
        using var lector = select.ExecuteReader();
        if (!lector.Read())
        {
            producto.IdProducto = -1;
        }
        else
        {
            producto.IdProducto = Convert.ToInt32(lector["id"]) + 1;
        }
        sql = "INSERT INTO Productos (idProducto,Descripcion,Precio) VALUES (@id,@descripcion,@precio)";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", producto.IdProducto));
        comando.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
        comando.ExecuteNonQuery();
        conexion.Close();
    }

    public void Update(Producto producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "UPDATE Productos SET Descripcion = @descripcion , Precio = @precio WHERE idProducto = @id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
        comando.Parameters.Add(new SqliteParameter("@id", producto.IdProducto));
        comando.ExecuteNonQuery();
        conexion.Close();
    }
}