using Microsoft.AspNetCore.Identity;

namespace MovieCatalog.Models;

public class MyRol : IdentityRole
{
    public string? Section{ get; set; }

    public DateTime AltaDate { get; set; }
}