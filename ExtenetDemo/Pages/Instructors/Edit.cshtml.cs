using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Instructors;

public class EditModel : InstructorCoursesPageModel
{
    private readonly SchoolContext _context;

    public EditModel(SchoolContext context)
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

        Vendor = await _context.Vendors
            .Include(i => i.OfficeAssignment)
            .Include(i => i.Courses)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Vendor == null)
        {
            return NotFound();
        }
        PopulateAssignedCourseData(_context, Vendor);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
    {
        if (id == null)
        {
            return NotFound();
        }

        var instructorToUpdate = await _context.Vendors
            .Include(i => i.OfficeAssignment)
            .Include(i => i.Courses)
            .FirstOrDefaultAsync(s => s.ID == id);

        if (instructorToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Vendor>(
            instructorToUpdate,
            "vendor",
            i => i.FirstMidName, i => i.LastName,
            i => i.HireDate, i => i.OfficeAssignment))
        {
            if (String.IsNullOrWhiteSpace(
                instructorToUpdate.OfficeAssignment?.Location))
            {
                instructorToUpdate.OfficeAssignment = null;
            }
            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        UpdateInstructorCourses(selectedCourses, instructorToUpdate);
        PopulateAssignedCourseData(_context, instructorToUpdate);
        return Page();
    }

    public void UpdateInstructorCourses(string[] selectedCourses,
                                        Vendor vendorToUpdate)
    {
        if (selectedCourses == null)
        {
            vendorToUpdate.Courses = new List<Item>();
            return;
        }

        var selectedCoursesHS = new HashSet<string>(selectedCourses);
        var instructorCourses = new HashSet<int>
            (vendorToUpdate.Courses.Select(c => c.ItemID));
        foreach (var course in _context.Items)
        {
            if (selectedCoursesHS.Contains(course.ItemID.ToString()))
            {
                if (!instructorCourses.Contains(course.ItemID))
                {
                    vendorToUpdate.Courses.Add(course);
                }
            }
            else
            {
                if (instructorCourses.Contains(course.ItemID))
                {
                    var courseToRemove = vendorToUpdate.Courses.Single(
                                                    c => c.ItemID == course.ItemID);
                    vendorToUpdate.Courses.Remove(courseToRemove);
                }
            }
        }
    }
}
