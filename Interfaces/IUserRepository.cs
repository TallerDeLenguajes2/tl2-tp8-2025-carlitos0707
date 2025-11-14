using tl2_tp8_2025_carlitos0707.Repositorios;
using tl2_tp8_2025_carlitos0707.Models;
namespace tl2_tp8_2025_carlitos0707.Interfaces;


public interface IUserRepository
{
    public Usuario GetUser(string usuario, string contrasena);
}