using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_carlitos0707.Interfaces;
using tl2_tp8_2025_carlitos0707.Models;

namespace tl2_tp8_2025_carlitos0707.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthenticationService authenticationService;
    public HomeController(ILogger<HomeController> logger,IAuthenticationService authentication)
    {
        _logger = logger;
        authenticationService = authentication;
    }

    public IActionResult Index()
    {
        if (!authenticationService.IsAuthenticated())
        {
            return RedirectToAction("Index","Login");
        }
        if (!(authenticationService.HasAccessLevel("Admin") || authenticationService.HasAccessLevel("Cliente")))
        {
            return RedirectToAction("Index","Login");
        }
        return View();
    }

}
