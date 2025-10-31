using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Productos;
using tl2_tp8_2025_carlitos0707.Models;
using PresupuestosRepository;
using Presupuestos;
namespace tl2_tp8_2025_carlitos0707.Controllers;

public class PresupuestosController : Controller
{
    private readonly PresupuestoRepository repo;
    private readonly ILogger<PresupuestosController> _logger;

    public PresupuestosController(ILogger<PresupuestosController> logger)
    {
        repo = new PresupuestoRepository();
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Presupuesto> presupuestos = repo.GetAll();
        return View(presupuestos);
    }
    public IActionResult Detalles(int id)
    {
        Presupuesto presupuesto = repo.ObtenerPorID(id);
        return View(presupuesto);   
    }
}
