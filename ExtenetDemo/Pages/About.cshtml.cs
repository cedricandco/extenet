using Extenet.Data;
using Extenet.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages;

public class AboutModel : PageModel
{
    private readonly SchoolContext _context;

    public AboutModel(SchoolContext context)
    {
        _context = context;
    }

    public IList<EnrollmentDateGroup> Students { get; set; }

    public async Task OnGetAsync()
    {
        IQueryable<EnrollmentDateGroup> data =
            from student in _context.Clients
            group student by student.EnrollmentDate into dateGroup
            select new EnrollmentDateGroup()
            {
                EnrollmentDate = dateGroup.Key,
                StudentCount = dateGroup.Count()
            };

        Students = await data.AsNoTracking().ToListAsync();
    }
}
