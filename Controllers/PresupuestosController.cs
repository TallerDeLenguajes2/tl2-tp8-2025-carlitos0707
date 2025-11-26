using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using tl2_tp8_2025_carlitos0707.Models;
using tl2_tp8_2025_carlitos0707.Repositorios;
using tl2_tp8_2025_carlitos0707.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using tl2_tp8_2025_carlitos0707.Interfaces;
namespace tl2_tp8_2025_carlitos0707.Controllers;

public class PresupuestosController : Controller
{
    private readonly IPresupuestoRepository repo;
    private readonly IProductoRepository repoProductos;
    private readonly IAuthenticationService authenticationService;
    private readonly ILogger<PresupuestosController> _logger;

    public PresupuestosController(ILogger<PresupuestosController> logger,IPresupuestoRepository r,IProductoRepository p,IAuthenticationService a)
    {
        repo = r;
        repoProductos = p;
        authenticationService = a;
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!(authenticationService.HasAccessLevel("Admin") || authenticationService.HasAccessLevel("Cliente")))
        {
            return View("AccesoDenegado");
        }
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
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!(authenticationService.HasAccessLevel("Admin") || authenticationService.HasAccessLevel("Cliente")))
        {
            return View("AccesoDenegado");
        }
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
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!authenticationService.HasAccessLevel("Admin"))
        {
            return View("AccesoDenegado");
        }
        CrearPresupuestoViewModel crearPresupuestoViewModel = new CrearPresupuestoViewModel();
        return View(crearPresupuestoViewModel);
    }


    [HttpPost]
    public IActionResult Create(CrearPresupuestoViewModel presupuesto)
    {
        if (!ModelState.IsValid)
        {
            presupuesto.MensajeError = "Ingrese todos los campos";
            return View("Create");
        }
        repo.CrearPresupuesto(new Presupuesto(presupuesto));
        return RedirectToAction("Index");
    }


    public IActionResult AgregarProducto(int id)
    {
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!authenticationService.HasAccessLevel("Admin"))
        {
            return View("AccesoDenegado");
        }
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
        if (!authenticationService.IsAuthenticated())
        {
            return View("Index","Login");
        }
        if (!authenticationService.HasAccessLevel("Admin"))
        {
            return View("AccesoDenegado");
        }
        DetallePresupuesto detalles = repo.GetProducto(idPresupuesto, idProducto);
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

    [HttpGet]
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
        PresupuestoViewModel presupuesto = new PresupuestoViewModel(repo.ObtenerPorID(id));
        return View(presupuesto);
    }


    [HttpPost]
    public IActionResult Delete(PresupuestoViewModel presupuesto)
    {
        repo.Eliminar(presupuesto.IdPresupuesto);
        return RedirectToAction("Index");
    }


    public IActionResult AccesoDenegado()
    {
        return View();
    }
}
