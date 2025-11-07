namespace PresupuestosRepository;

using System.Data.SqlTypes;
using Microsoft.Data.Sqlite;
using Presupuestos;
using PresupuestosDetalles;

public interface IPresupuestoRepository
{
    List<Presupuesto> GetAll();
    void CrearPresupuesto(Presupuesto presupuesto);
    void Eliminar(int id);
    Presupuesto ObtenerPorID(int id);
    void AgregarProducto(int IdPresupuesto, int IdProducto, int cantidad);
}

public class PresupuestoRepository : IPresupuestoRepository
{
    string cadenaConexion = "Data Source=Db/Tienda.db";
    public void AgregarProducto(int IdPresupuesto, int IdProducto, int cantidad)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "INSERT INTO PresupuestosDetalle (idPresupuesto,idProducto,Cantidad) VALUES (@idpres,@idprod,@cant)";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@idprod", IdProducto));
        comando.Parameters.Add(new SqliteParameter("@idpres", IdPresupuesto));
        comando.Parameters.Add(new SqliteParameter("@cant", cantidad));
        comando.ExecuteNonQuery();
        conexion.Close();

    }

    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "INSERT INTO Presupuestos (idPresupuesto,NombreDestinatario,FechaCreacion) VALUES (@id,@nombre,@fecha)";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", presupuesto.IdPresupuesto));
        comando.Parameters.Add(new SqliteParameter("@nombre", presupuesto.NombreDestinatario));
        comando.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion.ToString("yyyy-MM-dd")));
        comando.ExecuteNonQuery();
        conexion.Close();
    }

    public List<DetallePresupuesto> GetDetalles(int id)
    {
        ProductoRepository productoRepository = new ProductoRepository();
        List<DetallePresupuesto> detalles = new List<DetallePresupuesto>();
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "SELECT * FROM PresupuestosDetalle WHERE idPresupuesto = @id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            DetallePresupuesto detalle = new DetallePresupuesto
            {
                Cantidad = Convert.ToInt32(lector["Cantidad"]),
                producto = productoRepository.GetById(Convert.ToInt32(lector["idProducto"]))
            };
            detalles.Add(detalle);
        }
        conexion.Close();
        return detalles;
    }


    public void Update(Presupuesto presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "UPDATE Presupuestos SET NombreDestinatario = @nombre ,FechaCreacion = @fecha WHERE idPresupuesto = @id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@id",presupuesto.IdPresupuesto));
        comando.Parameters.Add(new SqliteParameter("@nombre", presupuesto.NombreDestinatario));
        comando.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion));
        comando.ExecuteNonQuery();
        conexion.Close();
    }

    public void EliminarDetalles(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));
        comando.ExecuteNonQuery();
        conexion.Close();
    }

    public void Eliminar(int id)
    {
        EliminarDetalles(id);
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));
        comando.ExecuteNonQuery();
        conexion.Close();

    }

    public List<Presupuesto> GetAll()
    {
        List<Presupuesto> presupuestos = new List<Presupuesto>();
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "SELECT * FROM Presupuestos";
        using var comando = new SqliteCommand(sql, conexion);
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            Presupuesto presupuesto = new Presupuesto
            {
                IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
                NombreDestinatario = lector["NombreDestinatario"].ToString(),
                FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"]),
                Detalles = GetDetalles(Convert.ToInt32(lector["idPresupuesto"]))
            };
            presupuestos.Add(presupuesto);
        }
        conexion.Close();
        return presupuestos;
    }

    public Presupuesto ObtenerPorID(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));
        using var lector = comando.ExecuteReader();
        if (!lector.Read())
        {
            return null;
        }
        Presupuesto presupuesto = new Presupuesto
        {
            IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
            NombreDestinatario = lector["NombreDestinatario"].ToString(),
            FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"]),
            Detalles = GetDetalles(Convert.ToInt32(id))
        };
        conexion.Close();
        return presupuesto;
    }
}