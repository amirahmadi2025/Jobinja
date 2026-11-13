using System.ComponentModel.DataAnnotations;

namespace Jobinja.DTOs;
public class RegisterDto
{
    [Required(ErrorMessage = "نام الزامی است")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "نام خانوادگی الزامی است")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "ایمیل الزامی است")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "رمز عبور الزامی است")]
    [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
    public string Password { get; set; } = null!;
}