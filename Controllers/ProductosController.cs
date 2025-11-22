using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using tl2_tp8_2025_carlitos0707.Models;
using tl2_tp8_2025_carlitos0707.ViewModel;
using tl2_tp8_2025_carlitos0707.Repositorios;
using tl2_tp8_2025_carlitos0707.Interfaces;
namespace tl2_tp8_2025_carlitos0707.Controllers;

public class ProductosController : Controller
{
    private readonly IProductoRepository repo;
    private readonly ILogger<ProductosController> _logger;
    private readonly IAuthenticationService authenticationService;

    public ProductosController(ILogger<ProductosController> logger,IProductoRepository r,IAuthenticationService a)
    {
        repo = r;
        _logger = logger;
        authenticationService = a;
    }

    public IActionResult Index()
    {
        if (!authenticationService.IsAuthenticated())
        {
            return RedirectToAction("Index","Login");
        }
        if (!authenticationService.HasAccessLevel("Admin"))
        {
            return View("AccesoDenegado");
        }
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
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!authenticationService.HasAccessLevel("Admin"))
        {
            return View("AccesoDenegado");
        }
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
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!authenticationService.HasAccessLevel("Admin"))
        {
            return View("AccesoDenegado");
        }
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
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!authenticationService.HasAccessLevel("Admin"))
        {
            return View("AccesoDenegado");
        }
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
