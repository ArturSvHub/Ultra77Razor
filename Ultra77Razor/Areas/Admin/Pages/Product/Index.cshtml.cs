using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.Product
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class IndexModel : PageModel
    {
        private readonly MssqlContext _context;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(MssqlContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
		[BindProperty]
        public List<UpakModelsLibrary.Models.Product> Products { get; set; }
        public void OnGet()
        {
            Products = _context.Products.Include(u => u.Category).ToList();
        }
    }
}
