using System;

namespace MovieCatalog.Models  // Asegúrate de que el namespace sea el correcto para tu proyecto
{
    public class Movie
    {
        public int Id { get; set; }             // Clave primaria (identificador único)
        public string Title { get; set; }       // Título de la película
        public string Genre { get; set; }       // Género de la película
        public int Year { get; set; }           // Año de lanzamiento
        public string Director { get; set; }    // Director de la película
        public string Description { get; set; } // Descripción de la película

        // Puedes agregar más propiedades si es necesario
    }
}
