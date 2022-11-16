using Extenet.Data;
using Extenet.Models;
using Extenet.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Extenet.Pages.Instructors;

public class InstructorCoursesPageModel : PageModel
{
    public List<AssignedCourseData> AssignedCourseDataList;

    public void PopulateAssignedCourseData(SchoolContext context,
                                           Vendor vendor)
    {
        var allCourses = context.Items;
        var instructorCourses = new HashSet<int>(
            vendor.Courses.Select(c => c.ItemID));
        AssignedCourseDataList = new List<AssignedCourseData>();
        foreach (var course in allCourses)
        {
            AssignedCourseDataList.Add(new AssignedCourseData
            {
                CourseID = course.ItemID,
                Title = course.Title,
                Assigned = instructorCourses.Contains(course.ItemID)
            });
        }
    }
}
