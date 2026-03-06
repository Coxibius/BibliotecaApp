using System;
using System.Collections.Generic;

namespace BibliotecaApp.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? ISBN { get; set; }
        public int AnioPublicacion { get; set; }
        public bool Disponible { get; set; } = true;
        
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }

        public List<Prestamo>? Prestamos { get; set; }
    }
}