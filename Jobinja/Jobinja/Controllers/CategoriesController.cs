using Jobinja.Data;
using Jobinja.DTOs;
using Jobinja.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jobinja.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // نمایش  تمام دسته‌بندی‌ها
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .ToListAsync();

        return Ok(categories);
    }

    // ثبت دسته‌بندی جدید
    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto dto)
    {
        var name = dto.Name.Trim();

        var exists = await _context.Categories
            .AnyAsync(c => c.Name == name);

        if (exists)
            return BadRequest("این دسته‌بندی قبلاً ثبت شده است");

        var category = new Category
        {
            Name = name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(category);
    }

    // ویرایش دسته‌بندی
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            return NotFound("دسته‌بندی پیدا نشد");

        var name = dto.Name.Trim();

        var exists = await _context.Categories
            .AnyAsync(c => c.Name == name && c.Id != id);

        if (exists)
            return BadRequest("این دسته‌بندی قبلاً ثبت شده است");

        category.Name = name;

        await _context.SaveChangesAsync();

        return Ok("دسته‌بندی با موفقیت ویرایش شد");
    }

    // حذف دسته‌بندی
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            return NotFound("دسته‌بندی پیدا نشد");

        bool hasJobs = await _context.Jobs
            .AnyAsync(j => j.CategoryId == id);

        if (hasJobs)
            return BadRequest(
                "این دسته‌بندی دارای آگهی استخدام است و قابل حذف نیست"
            );

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return Ok("دسته‌بندی با موفقیت حذف شد");
    }
}