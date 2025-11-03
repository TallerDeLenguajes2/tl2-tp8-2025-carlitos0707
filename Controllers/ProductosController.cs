using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Productos;
using tl2_tp8_2025_carlitos0707.Models;

namespace tl2_tp8_2025_carlitos0707.Controllers;

public class ProductosController : Controller
{
    private readonly ProductoRepository repo;
    private readonly ILogger<ProductosController> _logger;

    public ProductosController(ILogger<ProductosController> logger)
    {
        repo = new ProductoRepository();
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Producto> productos = repo.GetAll();
        return View(productos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public IActionResult Create(Producto producto)
    {
        repo.Insertar(producto);
        return View();
    }


    public IActionResult Edit(int id)
    {
        Producto producto = repo.GetById(id);
        return View(producto);
    }

    [HttpPut("Edit")]
    public IActionResult Edit(Producto producto)
    {
        repo.Update(producto);
        return View();
    }
}
