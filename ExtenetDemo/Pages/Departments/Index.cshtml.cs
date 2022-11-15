using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Departments;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;

    public IndexModel(SchoolContext context)
    {
        _context = context;
    }

    public IList<Department> Department { get;set; }

    public async Task OnGetAsync()
    {
        Department = await _context.Departments
            .Include(d => d.Administrator).ToListAsync();
    }
}
