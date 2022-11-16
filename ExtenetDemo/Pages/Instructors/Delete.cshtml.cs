using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Instructors;

public class DeleteModel : PageModel
{
    private readonly SchoolContext _context;

    public DeleteModel(SchoolContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Vendor Vendor { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Vendor = await _context.Vendors.FirstOrDefaultAsync(m => m.ID == id);

        if (Vendor == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Vendor vendor = await _context.Vendors
            .Include(i => i.Courses)
            .SingleAsync(i => i.ID == id);

        if (vendor == null)
        {
            return RedirectToPage("./Index");
        }

        var departments = await _context.Departments
            .Where(d => d.InstructorID == id)
            .ToListAsync();
        departments.ForEach(d => d.InstructorID = null);

        _context.Vendors.Remove(vendor);

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
