using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly MssqlContext _context;

		public DetailsModel(MssqlContext context)
		{
			_context = context;
		}

		[BindProperty]
        public IEnumerable<Product> Products{ get; set; }
		[BindProperty]
        public IEnumerable<Category> Categories { get; set; }
		[BindProperty]        
		public int CurrentId { get; set; }
		public void OnGet(int id)
        {
            CurrentId = id;
            Products = _context.Products.Include(u => u.Category);
            Categories = _context.Categories;
        }
    }
}
