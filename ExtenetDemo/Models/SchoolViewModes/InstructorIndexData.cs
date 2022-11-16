namespace Extenet.Models.SchoolViewModels;

public class InstructorIndexData
{
    public IEnumerable<Vendor> Instructors { get; set; }
    public IEnumerable<Item> Courses { get; set; }
    public IEnumerable<Sale> Enrollments { get; set; }
}
