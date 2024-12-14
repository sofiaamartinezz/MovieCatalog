using System;

namespace MovieCatalog.Models  // Asegúrate de que el namespace sea el correcto para tu proyecto
{
    public class User
    {
        public int Id { get; set; }              // Clave primaria (identificador único)
        public string Name { get; set; }         // Nombre del usuario
        public string Email { get; set; }        // Correo electrónico del usuario
        public string Password { get; set; }     // Contraseña del usuario
        public DateTime DateJoined { get; set; } // Fecha en que se unió el usuario a la aplicación

        // Puedes agregar más propiedades si es necesario
    }
}
