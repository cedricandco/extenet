using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;

    public IndexModel(SchoolContext context)
    {
        _context = context;
    }

    public IList<Item> Courses { get; set; }

    public async Task OnGetAsync()
    {
        Courses = await _context.Items
            .Include(c => c.Department)
            .AsNoTracking()
            .ToListAsync();
    }
}
