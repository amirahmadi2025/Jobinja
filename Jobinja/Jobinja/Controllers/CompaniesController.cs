using Jobinja.Data;
using Jobinja.DTOs;
using Jobinja.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jobinja.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CompaniesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompaniesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ثبت شرکت
    [HttpPost("register")]
    public async Task<IActionResult> RegisterCompany(CreateCompanyDto dto)
    {
        // دریافت شناسه کاربر از JWT
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        // ساخت شرکت
        var company = new Company
        {
            Name = dto.Name,
            Description = dto.Description,
            Website = dto.Website,
            Address = dto.Address,
            UserId = userId
        };

        // ذخیره در دیتابیس
        _context.Companies.Add(company);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "شرکت با موفقیت ثبت شد",
            companyId = company.Id,
            companyName = company.Name
        });
    }
}