using Microsoft.AspNetCore.Builder;

namespace Jobinja.Entities;

public class Job
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Salary { get; set; }

    public string Location { get; set; } = null!;

    public string EmploymentType { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ExpireDate { get; set; }

    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    
}