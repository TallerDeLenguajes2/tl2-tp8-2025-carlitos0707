using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_carlitos0707.Models;
using tl2_tp8_2025_carlitos0707.ViewModel;
using tl2_tp8_2025_carlitos0707.Repositorios;
using tl2_tp8_2025_carlitos0707.Interfaces;
namespace tl2_tp8_2025_carlitos0707.Controllers;

public class ProductosController : Controller
{
    private readonly IProductoRepository repo;
    private readonly ILogger<ProductosController> _logger;

    public ProductosController(ILogger<ProductosController> logger,IProductoRepository r)
    {
        repo = r;
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Producto> productos = repo.GetAll();
        List<ProductoViewModel> productosViewModels = new List<ProductoViewModel>();

        foreach (Producto producto in productos)
        {
            productosViewModels.Add(new ProductoViewModel(producto));
        }
        return View(productosViewModels);
    }

    public IActionResult Create()
    {
        CrearProductoViewModel crearProductoViewModel = new CrearProductoViewModel();
        return View(crearProductoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductoViewModel producto)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Create");
        }
        repo.Insertar(new Producto(producto));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Producto producto = repo.GetById(id);
        if (producto is null)
        {
            RedirectToAction("Index");
        }
        ProductoViewModel productoViewModel = new ProductoViewModel(producto);
        return View(productoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProductoViewModel producto)
    {
        repo.Update(new Producto(producto));
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        Producto producto = repo.GetById(id);
        if (producto is null)
        {
            return RedirectToAction("Index");
        }
        ProductoViewModel productoViewModel = new ProductoViewModel(producto);
        return View(productoViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(ProductoViewModel producto)
    {
        repo.Delete(producto.IdProducto);
        return RedirectToAction("Index");
    }
}
