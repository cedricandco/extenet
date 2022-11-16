using System.ComponentModel.DataAnnotations;

namespace Extenet.Models;

public enum Grade
{
    A, B, C, D, F
}

public class Sale
{
    public int SaleID { get; set; }
    public int ItemID { get; set; }
    public int ClientID { get; set; }
    [DisplayFormat(NullDisplayText = "No grade")]
    public Grade? Grade { get; set; }

    public Item Item { get; set; }
    public Client Client { get; set; }
}
