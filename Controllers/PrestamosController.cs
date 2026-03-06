using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly BibliotecaContext _context;

        public PrestamosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewBag.EstudianteId = new SelectList(_context.Estudiantes, "Id", "Nombre");
            
            // 1) En el método GET de Create, el SelectList de Libros solo debe mostrar libros donde Disponible == true.
            var librosDisponibles = _context.Libros.Where(l => l.Disponible).ToList();
            ViewBag.LibroId = new SelectList(librosDisponibles, "Id", "Titulo");
            
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaPrestamo,FechaDevolucion,LibroId,EstudianteId")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                // 2) En el método POST de Create, al guardar el préstamo, actualiza el libro seleccionado cambiando su estado Disponible a false.
                var libroSeleccionado = await _context.Libros.FindAsync(prestamo.LibroId);
                if (libroSeleccionado != null)
                {
                    libroSeleccionado.Disponible = false;
                    _context.Update(libroSeleccionado);
                }

                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.EstudianteId = new SelectList(_context.Estudiantes, "Id", "Nombre", prestamo.EstudianteId);
            
            var librosDisponibles = _context.Libros.Where(l => l.Disponible).ToList();
            ViewBag.LibroId = new SelectList(librosDisponibles, "Id", "Titulo", prestamo.LibroId);
            
            return View(prestamo);
        }

        // POST: Prestamos/Devolver/5
        // 3) Crea una acción llamada Devolver(int id) que le asigne DateTime.Now a FechaDevolucion, cambie el libro asociado a Disponible = true y guarde los cambios en la base de datos.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            if (prestamo.FechaDevolucion == null)
            {
                prestamo.FechaDevolucion = DateTime.Now;

                if (prestamo.Libro != null)
                {
                    prestamo.Libro.Disponible = true;
                    _context.Update(prestamo.Libro);
                }

                _context.Update(prestamo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            
            ViewBag.EstudianteId = new SelectList(_context.Estudiantes, "Id", "Nombre", prestamo.EstudianteId);
            
            // Para editar, mostramos todos los libros o podrías lógica adicional si quisieras
            // pero lo más seguro es mostrar todo para no romper relaciones actuales, o solo el actual y los disponibles.
            // Mostraré todos para evitar problemas si se guarda al mismo libro que ya no está "Disponible", porque está prestado en este préstamo.
            var librosParaEditar = _context.Libros.Where(l => l.Disponible || l.Id == prestamo.LibroId).ToList();
            ViewBag.LibroId = new SelectList(librosParaEditar, "Id", "Titulo", prestamo.LibroId);
            
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaPrestamo,FechaDevolucion,LibroId,EstudianteId")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.EstudianteId = new SelectList(_context.Estudiantes, "Id", "Nombre", prestamo.EstudianteId);
            
            var librosParaEditar = _context.Libros.Where(l => l.Disponible || l.Id == prestamo.LibroId).ToList();
            ViewBag.LibroId = new SelectList(librosParaEditar, "Id", "Titulo", prestamo.LibroId);
            
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(p => p.Id == id);
                
            if (prestamo != null)
            {
                // Si eliminamos un préstamo no devuelto, devolvamos el estado original al libro
                if (prestamo.FechaDevolucion == null && prestamo.Libro != null)
                {
                    prestamo.Libro.Disponible = true;
                    _context.Update(prestamo.Libro);
                }

                _context.Prestamos.Remove(prestamo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
