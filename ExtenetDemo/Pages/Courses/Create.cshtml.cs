using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Extenet.Pages.Courses;

public class CreateModel : DepartmentNamePageModel
{
    private readonly SchoolContext _context;

    public CreateModel(SchoolContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        PopulateDepartmentsDropDownList(_context);
        return Page();
    }

    [BindProperty]
    public Item Item { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var emptyCourse = new Item();

        if (await TryUpdateModelAsync<Item>(
             emptyCourse,
             "item",   // Prefix for form value.
             s => s.ItemID, s => s.DepartmentID, s => s.Title, s => s.Price))
        {
            _context.Items.Add(emptyCourse);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        // Select DepartmentID if TryUpdateModelAsync fails.
        PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentID);
        return Page();
    }
}
