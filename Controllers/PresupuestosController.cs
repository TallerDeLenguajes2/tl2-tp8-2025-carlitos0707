using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_carlitos0707.Models;
using PresupuestosRepository;
using tl2_tp8_2025_carlitos0707.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace tl2_tp8_2025_carlitos0707.Controllers;

public class PresupuestosController : Controller
{
    private readonly PresupuestoRepository repo;
    private readonly ProductoRepository repoProductos;
    private readonly ILogger<PresupuestosController> _logger;

    public PresupuestosController(ILogger<PresupuestosController> logger)
    {
        repo = new PresupuestoRepository();
        repoProductos = new ProductoRepository();
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Presupuesto> presupuestos = repo.GetAll();
        List<PresupuestoViewModel> presupuestosViewModels = new List<PresupuestoViewModel>();

        foreach (Presupuesto item in presupuestos)
        {
            presupuestosViewModels.Add(new PresupuestoViewModel(item));
        }
        return View(presupuestosViewModels);
    }
    public IActionResult Detalles(int id)
    {
        Presupuesto presupuesto = repo.ObtenerPorID(id);
        if (presupuesto is null)
        {
            return RedirectToAction("Index");
        }

        PresupuestoViewModel presupuestoViewModel = new PresupuestoViewModel(presupuesto);
        return View(presupuestoViewModel);
    }


    [HttpGet]
    public IActionResult Create()
    {
        CrearPresupuestoViewModel crearPresupuestoViewModel = new CrearPresupuestoViewModel();
        return View(crearPresupuestoViewModel);
    }


    [HttpPost]
    public IActionResult Create(CrearPresupuestoViewModel presupuesto)
    {
        repo.CrearPresupuesto(new Presupuesto(presupuesto));
        return RedirectToAction("Index");
    }


    public IActionResult AgregarProducto(int id)
    {
        Presupuesto presupuesto = repo.ObtenerPorID(id);
        if (presupuesto is null)
        {
            return RedirectToAction("Index");
        }
        List<Producto> productos = repoProductos.GetAll();
        AgregarProductoViewModel agregarProductoViewModel = new AgregarProductoViewModel
        {
            IdPresupuesto = presupuesto.IdPresupuesto,
            productos = new SelectList(productos, "IdProducto", "Descripcion")
        };

        return View(agregarProductoViewModel);
    }


    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel agregarProductoViewModel)
    {
        repo.AgregarProducto(agregarProductoViewModel.IdPresupuesto, agregarProductoViewModel.IdProducto, agregarProductoViewModel.Cantidad);

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult BorrarProducto(int idPresupuesto, int idProducto)
    {
        DetallesPresupuestoViewModel detalles = repo.GetProducto(idPresupuesto, idProducto);
        if (detalles is null)
        {
            return View();
        }
        return View(detalles);
    }

    [HttpPost]
    public IActionResult BorrarProducto(DetallesPresupuestoViewModel detalles)
    {
        repo.BorrarProducto(detalles);
        return RedirectToAction("Index");
    }



    public IActionResult Edit(int id)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        PresupuestoViewModel presupuesto = new PresupuestoViewModel(repo.ObtenerPorID(id));
        return View(presupuesto);
    }


    [HttpPost]
    public IActionResult Edit(PresupuestoViewModel presupuesto)
    {
        repo.Update(new Presupuesto(presupuesto));
        return RedirectToAction("Index");
    }


    public IActionResult Delete(int id)
    {
        PresupuestoViewModel presupuesto = new PresupuestoViewModel(repo.ObtenerPorID(id));
        return View(presupuesto);
    }


    [HttpPost]
    public IActionResult Delete(PresupuestoViewModel presupuesto)
    {
        repo.Eliminar(presupuesto.IdPresupuesto);
        return RedirectToAction("Index");
    }
}
