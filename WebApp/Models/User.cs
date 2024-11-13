using System.ComponentModel.DataAnnotations;

public class User 
{
    [Key]
    [Required]
    public string Email {get; set;}
    
    [Required]
    public string FirstName {get; set;}

    [Required]
    public string LastName {get; set;}

    [Required]
    public string Country {get; set;}

    [Required]
    public string Password {get; set;}
}