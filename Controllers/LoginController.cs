using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using tl2_tp8_2025_carlitos0707.Models;
using tl2_tp8_2025_carlitos0707.Interfaces;
using tl2_tp8_2025_carlitos0707.ViewModel;
namespace tl2_tp8_2025_carlitos0707.Controllers;

public class LoginController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<LoginController> _logger;
    public LoginController(IAuthenticationService authenticationService,ILogger<LoginController> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }
    // [HttpGet] Muestra la vista de login
    public IActionResult Index()
    {
    // ... (Crear LoginViewModel)
    return View(new LoginViewModel());
    }
    // [HttpPost] Procesa el login
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            model.ErrorMessage = "Debe ingresar usuario y contraseña.";
            return View("Index", model);
        }
        if (_authenticationService.Login(model.Username, model.Password))
        {
            return RedirectToAction("Index", "Home");
        }
        model.ErrorMessage = "Credenciales inválidas.";
        return View("Index",model);
        //return RedirectToAction("Index", "Home");
    }
    // [HttpGet] Cierra sesión
    public IActionResult Logout()
    {
        _authenticationService.Logout();
        return RedirectToAction("Index");
    }
}