using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace WebApp.Models
{
    public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public int CountryId { get; set; }
    public Country Country { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    public void HashPassword()
    {
        var passwordHasher = new PasswordHasher<User>();
        Password = passwordHasher.HashPassword(this, Password);
    }
}

}
