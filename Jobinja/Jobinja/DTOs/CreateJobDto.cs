using System.ComponentModel.DataAnnotations;

namespace Jobinja.DTOs;

public class CreateJobDto
{
    [Required(ErrorMessage = "عنوان شغل الزامی است")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "توضیحات آگهی الزامی است")]
    public string Description { get; set; } = null!;

    public string? Salary { get; set; }

    [Required(ErrorMessage = "محل کار الزامی است")]
    public string Location { get; set; } = null!;

    [Required(ErrorMessage = "نوع همکاری الزامی است")]
    public string EmploymentType { get; set; } = null!;

    public DateTime? ExpireDate { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int CompanyId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}