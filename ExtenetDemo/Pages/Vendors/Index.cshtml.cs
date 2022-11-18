using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Vendors;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;
    private readonly IConfiguration Configuration;

    public IndexModel(SchoolContext context, IConfiguration configuration)
    {
        _context = context;
        Configuration = configuration;
    }

    public string NameSort { get; set; }
    public string DateSort { get; set; }
    public string CurrentFilter { get; set; }
    public string CurrentSort { get; set; }

    public PaginatedList<Vendor> Vendors { get; set; }

    public async Task OnGetAsync(string sortOrder,
        string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        DateSort = sortOrder == "Date" ? "date_desc" : "Date";
        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        IQueryable<Vendor> vendorsIQ = from s in _context.Vendors
                                         select s;
        if (!String.IsNullOrEmpty(searchString))
        {
            vendorsIQ = vendorsIQ.Where(s => s.LastName.Contains(searchString)
                                   || s.FirstMidName.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "name_desc":
                vendorsIQ = vendorsIQ.OrderByDescending(s => s.LastName);
                break;
            //case "Date":
            //    vendorsIQ = vendorsIQ.OrderBy(s => s.EnrollmentDate);
            //    break;
            //case "date_desc":
            //    vendorsIQ = vendorsIQ.OrderByDescending(s => s.EnrollmentDate);
            //    break;
            default:
                vendorsIQ = vendorsIQ.OrderBy(s => s.LastName);
                break;
        }

        var pageSize = Configuration.GetValue("PageSize", 4);
        Vendors = await PaginatedList<Vendor>.CreateAsync(
            vendorsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}
