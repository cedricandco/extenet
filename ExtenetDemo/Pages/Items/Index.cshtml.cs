using Extenet.Data;
using Extenet.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Pages.Items;

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

    public PaginatedList<Item> Items { get; set; }

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

        IQueryable<Item> studentsIQ = from s in _context.Items
                                        select s;
        if (!String.IsNullOrEmpty(searchString))
        {
            studentsIQ = studentsIQ.Where(s => s.Price.ToString().Contains(searchString)
                                   || s.Title.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "name_desc":
                studentsIQ = studentsIQ.OrderByDescending(s => s.Price);
                break;
            case "Date":
                studentsIQ = studentsIQ.OrderBy(s => s.ItemID);
                break;
            case "date_desc":
                studentsIQ = studentsIQ.OrderByDescending(s => s.Title);
                break;
            default:
                studentsIQ = studentsIQ.OrderBy(s => s.ItemID);
                break;
        }

        var pageSize = Configuration.GetValue("PageSize", 4);
        Items = await PaginatedList<Item>.CreateAsync(
            studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}
