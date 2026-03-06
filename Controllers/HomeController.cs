using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BibliotecaContext _context;

    public HomeController(ILogger<HomeController> logger, BibliotecaContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var librosDisponibles = await _context.Libros
            .Include(l => l.Autor)
            .Where(l => l.Disponible)
            .ToListAsync();

        return View(librosDisponibles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
