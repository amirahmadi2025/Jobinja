using System.ComponentModel.DataAnnotations;

namespace Jobinja.DTOs;


public class LoginDto
{
    [Required(ErrorMessage = "ایمیل الزامی است")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "رمز عبور الزامی است")]
    public string Password { get; set; } = null!;
}