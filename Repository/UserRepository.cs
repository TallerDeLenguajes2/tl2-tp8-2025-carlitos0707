using Microsoft.Data.Sqlite;
using tl2_tp8_2025_carlitos0707.Interfaces;
using tl2_tp8_2025_carlitos0707.Models;
namespace tl2_tp8_2025_carlitos0707.Repositorios;

public class UsuarioRepository : IUserRepository
{
    readonly string CadenaConexion = "Db/Tienda.db";
 // Lógica para conectar con la DB y buscar por user/pass.
    public Usuario GetUser(string usuario, string contrasena)
    {
        Usuario user = null;
        //Consulta SQL que busca por Usuario Y Contrasena
        const string sql = @"
        SELECT Id, Nombre, User, Pass, Rol
        FROM Usuarios
        WHERE User = @Usuario AND Pass = @Contrasena";
        using var conexion = new SqliteConnection(CadenaConexion);
        conexion.Open();
        using var comando = new SqliteCommand(sql, conexion);

        // Se usan parámetros para
        comando.Parameters.AddWithValue("@Usuario", usuario);
        comando.Parameters.AddWithValue("@Contrasena",contrasena);
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
        // Si el lector encuentra una fila, el usuario existe y las credenciales son correctas
        user = new Usuario
        {
        Id = reader.GetInt32(0),
        Nombre = reader.GetString(1),
        User = reader.GetString(2),
        Pass = reader.GetString(3),
        Rol = reader.GetString(4)
        };
        }
    return user;
    }
}