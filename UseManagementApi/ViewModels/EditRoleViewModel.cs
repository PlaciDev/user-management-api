using System.ComponentModel.DataAnnotations;

namespace UseManagementApi.ViewModels;

public class EditRoleViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 14 caracteres.")]
    public string Name { get; set; }
}