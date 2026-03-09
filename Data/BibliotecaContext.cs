using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Models;

namespace BibliotecaApp.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Seed: 5 Autores (IDs 101-105 para evitar conflictos) ─────
            modelBuilder.Entity<Autor>().HasData(
                new Autor
                {
                    Id = 101,
                    Nombre = "J.K. Rowling",
                    Nacionalidad = "Británica",
                    FechaNacimiento = new DateTime(1965, 7, 31)
                },
                new Autor
                {
                    Id = 102,
                    Nombre = "Gabriel García Márquez",
                    Nacionalidad = "Colombiana",
                    FechaNacimiento = new DateTime(1927, 3, 6)
                },
                new Autor
                {
                    Id = 103,
                    Nombre = "J.R.R. Tolkien",
                    Nacionalidad = "Británica",
                    FechaNacimiento = new DateTime(1892, 1, 3)
                },
                new Autor
                {
                    Id = 104,
                    Nombre = "George Orwell",
                    Nacionalidad = "Británica",
                    FechaNacimiento = new DateTime(1903, 6, 25)
                },
                new Autor
                {
                    Id = 105,
                    Nombre = "Isabel Allende",
                    Nacionalidad = "Chilena",
                    FechaNacimiento = new DateTime(1942, 8, 2)
                }
            );

            // ── Seed: 10 Libros (IDs 101-110 para evitar conflictos) ─────
            modelBuilder.Entity<Libro>().HasData(
                new Libro
                {
                    Id = 101,
                    Titulo = "Harry Potter y la Piedra Filosofal",
                    ISBN = "978-8478884452",
                    AnioPublicacion = 1997,
                    Disponible = true,
                    AutorId = 101
                },
                new Libro
                {
                    Id = 102,
                    Titulo = "Harry Potter y la Cámara Secreta",
                    ISBN = "978-8478884469",
                    AnioPublicacion = 1998,
                    Disponible = true,
                    AutorId = 101
                },
                new Libro
                {
                    Id = 103,
                    Titulo = "Cien Años de Soledad",
                    ISBN = "978-0060883287",
                    AnioPublicacion = 1967,
                    Disponible = true,
                    AutorId = 102
                },
                new Libro
                {
                    Id = 104,
                    Titulo = "El Amor en los Tiempos del Cólera",
                    ISBN = "978-0307389732",
                    AnioPublicacion = 1985,
                    Disponible = true,
                    AutorId = 102
                },
                new Libro
                {
                    Id = 105,
                    Titulo = "El Señor de los Anillos: La Comunidad del Anillo",
                    ISBN = "978-8445073704",
                    AnioPublicacion = 1954,
                    Disponible = true,
                    AutorId = 103
                },
                new Libro
                {
                    Id = 106,
                    Titulo = "El Hobbit",
                    ISBN = "978-8445073858",
                    AnioPublicacion = 1937,
                    Disponible = true,
                    AutorId = 103
                },
                new Libro
                {
                    Id = 107,
                    Titulo = "1984",
                    ISBN = "978-0451524935",
                    AnioPublicacion = 1949,
                    Disponible = true,
                    AutorId = 104
                },
                new Libro
                {
                    Id = 108,
                    Titulo = "Rebelión en la Granja",
                    ISBN = "978-0451526342",
                    AnioPublicacion = 1945,
                    Disponible = true,
                    AutorId = 104
                },
                new Libro
                {
                    Id = 109,
                    Titulo = "La Casa de los Espíritus",
                    ISBN = "978-8401352508",
                    AnioPublicacion = 1982,
                    Disponible = true,
                    AutorId = 105
                },
                new Libro
                {
                    Id = 110,
                    Titulo = "Eva Luna",
                    ISBN = "978-8401352966",
                    AnioPublicacion = 1987,
                    Disponible = true,
                    AutorId = 105
                }
            );
        }
    }
}