using Extenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Students;

public class DetailsModel : PageModel
{
    private readonly Extenet.Data.SchoolContext _context;

    public DetailsModel(Extenet.Data.SchoolContext context)
    {
        _context = context;
    }

    public Client Client { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Client = await _context.Clients
            .Include(s => s.Sales)
            .ThenInclude(e => e.Item)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Client == null)
        {
            return NotFound();
        }
        return Page();
    }
}
