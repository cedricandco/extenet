using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Courses;

public class DeleteModel : PageModel
{
    private readonly SchoolContext _context;

    public DeleteModel(SchoolContext context)
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
            .AsNoTracking()
            .Include(c => c.Department)
            .FirstOrDefaultAsync(m => m.ItemID == id);

        if (Item == null)
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

        Item = await _context.Items.FindAsync(id);

        if (Item != null)
        {
            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
