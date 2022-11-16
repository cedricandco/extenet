using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Courses;

public class EditModel : DepartmentNamePageModel
{
    private readonly SchoolContext _context;

    public EditModel(SchoolContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Item Item { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Item = await _context.Items
            .Include(c => c.Department).FirstOrDefaultAsync(m => m.ItemID == id);

        if (Item == null)
        {
            return NotFound();
        }

        // Select current DepartmentID.
        PopulateDepartmentsDropDownList(_context, Item.DepartmentID);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var courseToUpdate = await _context.Items.FindAsync(id);

        if (courseToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Item>(
             courseToUpdate,
             "item",   // Prefix for form value.
               c => c.Price, c => c.DepartmentID, c => c.Title))
        {
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        // Select DepartmentID if TryUpdateModelAsync fails.
        PopulateDepartmentsDropDownList(_context, courseToUpdate.DepartmentID);
        return Page();
    }
}
