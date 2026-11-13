using System.ComponentModel.DataAnnotations;

namespace Jobinja.DTOs;


public class CategoryDto
{
    [Required(ErrorMessage = "نام دسته‌بندی الزامی است")]
    public string Name { get; set; } = null!;
}