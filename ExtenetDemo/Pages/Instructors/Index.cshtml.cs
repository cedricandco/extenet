using Extenet.Data;
using Extenet.Models;
using Extenet.Models.SchoolViewModels;  // Add VM
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Instructors;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;

    public IndexModel(SchoolContext context)
    {
        _context = context;
    }

    public InstructorIndexData InstructorData { get; set; }
    public int InstructorID { get; set; }
    public int CourseID { get; set; }

    public async Task OnGetAsync(int? id, int? courseID)
    {
        InstructorData = new InstructorIndexData();
        InstructorData.Instructors = await _context.Vendors
            .Include(i => i.OfficeAssignment)
            .Include(i => i.Courses)
                .ThenInclude(c => c.Department)
            .OrderBy(i => i.LastName)
            .ToListAsync();

        if (id != null)
        {
            InstructorID = id.Value;
            Vendor vendor = InstructorData.Instructors
                .Where(i => i.ID == id.Value).Single();
            InstructorData.Courses = vendor.Courses;
        }

        if (courseID != null)
        {
            CourseID = courseID.Value;
            var selectedCourse = InstructorData.Courses
                .Where(x => x.ItemID == courseID).Single();
            await _context.Entry(selectedCourse)
                          .Collection(x => x.Sales).LoadAsync();
            foreach (Sale enrollment in selectedCourse.Sales)
            {
                await _context.Entry(enrollment).Reference(x => x.Client).LoadAsync();
            }
            InstructorData.Enrollments = selectedCourse.Sales;
        }
    }
}
