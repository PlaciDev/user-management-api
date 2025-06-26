using System.ComponentModel.DataAnnotations;
using UseManagementApi.Models;

namespace UseManagementApi.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email é inválido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(10, ErrorMessage = "A senha deve conter no minimo 10 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&\-_])[A-Za-z\d@$!%*?&\-_]{10,}$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiuscula, uma letra minúscula, um número e um caractere especia.")]
    public string Password { get; set; }
}