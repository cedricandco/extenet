using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Extenet.Models;

public class Item
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Number")]
    public int ItemID { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Title { get; set; }

    public int Price { get; set; }

    public int DepartmentID { get; set; }

    public Department Department { get; set; }
    public ICollection<Sale> Sales { get; set; }
    public ICollection<Vendor> Vendors { get; set; }
}
