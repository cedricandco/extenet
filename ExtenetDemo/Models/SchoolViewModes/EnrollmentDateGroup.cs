using System.ComponentModel.DataAnnotations;

namespace Extenet.Models.SchoolViewModels;

public class EnrollmentDateGroup
{
    [DataType(DataType.Date)]
    public DateTime? EnrollmentDate { get; set; }

    public int StudentCount { get; set; }
}
