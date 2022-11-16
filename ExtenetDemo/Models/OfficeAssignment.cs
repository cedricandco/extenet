using System.ComponentModel.DataAnnotations;

namespace Extenet.Models;

public class OfficeAssignment
{
    [Key]
    public int VendorID { get; set; }
    [StringLength(50)]
    [Display(Name = "Office Location")]
    public string Location { get; set; }

    public Vendor Vendor { get; set; }
}
