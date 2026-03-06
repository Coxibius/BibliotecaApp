using System;

namespace BibliotecaApp.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
        public DateTime? FechaDevolucion { get; set; }
        
        public int LibroId { get; set; }
        public Libro? Libro { get; set; }

        public int EstudianteId { get; set; }
        public Estudiante? Estudiante { get; set; }
    }
}