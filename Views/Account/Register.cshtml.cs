using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Data;
using MovieCatalog.Models;

namespace MovieCatalog.Views.Account;
public class RegisterModel : PageModel
{
    private readonly UserManager<MyUser> _userManager;

    [BindProperty]
    public RegisterDTO Register { get; set; }
    public string ReturnUrl { get; set; }

    public RegisterModel(UserManager<MyUser> userManager)
    => 
        _userManager = userManager;

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Register.Password != Register.Password2){
            throw new Exception("Passwords don't match");
        }

        //create new user
        var user = new MyUser();
        user.Email = Register.Email;
        user.UserName = Register.Email;

        //create new user in database
        var res = await _userManager.CreateAsync(user, Register.Password);

        // redirect new user to página de inicio
        if(ReturnUrl == null) {
            ReturnUrl = "/";
        }

        return LocalRedirect(ReturnUrl);
    }
}