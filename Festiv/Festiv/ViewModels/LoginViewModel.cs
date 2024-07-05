using System.ComponentModel.DataAnnotations;

namespace Festiv.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage ="Username required for Festivities")]
    public string? Username {get; set;}

    [DataType(DataType.Password)]
    [Required(ErrorMessage ="Password required for Festivities")]
    public string? Password {get; set;}
    
    [Display(Name ="Remember Me")]
    public bool RememberMe {get; set;}
}
