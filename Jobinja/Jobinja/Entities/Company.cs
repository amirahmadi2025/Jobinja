namespace Jobinja.Entities;

public class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? Address { get; set; }

    public string UserId { get; set; } = null!;

    public User Owner { get; set; } = null!;

    public List<Job> Jobs { get; set; } = new();
}