using Jobinja.Data;
using Jobinja.DTOs;
using Jobinja.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Jobinja.Controllers;
[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. مشاهده تمام آگهی‌ها
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var jobs = await _context.Jobs
            .AsNoTracking()
            .Select(j => new
            {
                j.Id,
                j.Title,
                j.Description,
                j.Salary,
                j.Location,
                j.EmploymentType,
                j.CreatedAt,
                j.ExpireDate,
                j.CompanyId,
                CompanyName = j.Company.Name,
                j.CategoryId,
                CategoryName = j.Category.Name
            })
            .ToListAsync();

        return Ok(jobs);
    }

    // 2. ثبت آگهی استخدام
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateJobDto dto)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (userId == null)
            return Unauthorized();

        // بررسی مالکیت شرکت
        var isOwner = await _context.Companies
            .AnyAsync(c =>
                c.Id == dto.CompanyId &&
                c.UserId == userId
            );

        if (!isOwner)
            return Forbid();

        // بررسی وجود دسته‌بندی
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
            return BadRequest("دسته‌بندی موردنظر وجود ندارد");

        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            Salary = dto.Salary,
            Location = dto.Location,
            EmploymentType = dto.EmploymentType,
            ExpireDate = dto.ExpireDate,
            CompanyId = dto.CompanyId,
            CategoryId = dto.CategoryId
        };

        _context.Jobs.Add(job);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "آگهی استخدام با موفقیت ثبت شد",
            jobId = job.Id
        });
    }

    // 3. ویرایش آگهی استخدام
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateJobDto dto)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (userId == null)
            return Unauthorized();

        var job = await _context.Jobs
            .FirstOrDefaultAsync(j => j.Id == id);

        if (job == null)
            return NotFound("آگهی استخدام پیدا نشد");

        // بررسی مالکیت شرکت
        var isOwner = await _context.Companies
            .AnyAsync(c =>
                c.Id == job.CompanyId &&
                c.UserId == userId
            );

        if (!isOwner)
            return Forbid();

        // بررسی شرکت جدید
        var newCompanyIsOwner = await _context.Companies
            .AnyAsync(c =>
                c.Id == dto.CompanyId &&
                c.UserId == userId
            );

        if (!newCompanyIsOwner)
            return Forbid();

        // بررسی دسته‌بندی
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
            return BadRequest("دسته‌بندی موردنظر وجود ندارد");

        job.Title = dto.Title;
        job.Description = dto.Description;
        job.Salary = dto.Salary;
        job.Location = dto.Location;
        job.EmploymentType = dto.EmploymentType;
        job.ExpireDate = dto.ExpireDate;
        job.CompanyId = dto.CompanyId;
        job.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return Ok("آگهی با موفقیت ویرایش شد");
    }

    // 4. حذف آگهی استخدام
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (userId == null)
            return Unauthorized();

        var job = await _context.Jobs
            .FirstOrDefaultAsync(j => j.Id == id);

        if (job == null)
            return NotFound("آگهی استخدام پیدا نشد");

        // بررسی مالکیت شرکت
        var isOwner = await _context.Companies
            .AnyAsync(c =>
                c.Id == job.CompanyId &&
                c.UserId == userId
            );

        if (!isOwner)
            return Forbid();

        _context.Jobs.Remove(job);

        await _context.SaveChangesAsync();

        return Ok("آگهی با موفقیت حذف شد");
    }




    

}