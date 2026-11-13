using System.ComponentModel.DataAnnotations;

namespace Jobinja.DTOs;
public class CreateCompanyDto
{
    [Required(ErrorMessage = "نام شرکت الزامی است")]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? Address { get; set; }
}