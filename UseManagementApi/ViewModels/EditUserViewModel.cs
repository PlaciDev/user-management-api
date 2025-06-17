using System.ComponentModel.DataAnnotations;
using UseManagementApi.Models;

namespace UseManagementApi.ViewModels;

public class EditUserViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50, MinimumLength = 5 , ErrorMessage = "O nome deve conter entre 5 e 50 caracteres.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [StringLength(70)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(12, ErrorMessage = "A senha deve conter no mínimo 12 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])",
        ErrorMessage = "A senha deve conter: 1 letra minúscula, 1 letra maiuscula, 1 número, 1 caracter especial.")]
    public string Password { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [Required]
    public Role Role { get; set; }
}