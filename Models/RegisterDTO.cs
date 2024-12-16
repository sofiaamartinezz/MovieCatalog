namespace MovieCatalog.Models;
// lo que le pedimos al usuario para poder registrarse
public class RegisterDTO
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Password2 { get; set; }


}