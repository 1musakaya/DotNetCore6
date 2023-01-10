using System.ComponentModel.DataAnnotations;

namespace WebApplicationCoreLogin.Models.ViewModel
{
    public class RegisterViewModel
    {
        
            [Required(ErrorMessage = "Kullanıcı Adı zorunludur.")]
            [StringLength(30, ErrorMessage = "Kullanıcı Adı maksimum 30 karakter olmalıdır.")]
            public string Username { get; set; }

            [Required]
            [MinLength(6)]
            [MaxLength(16)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
    
            [Required]
            [MinLength(6)]
            [MaxLength(16)]
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            public string Password2 { get; set; }    



    }
}
