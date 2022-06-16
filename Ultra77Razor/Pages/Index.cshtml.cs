using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;
using UpakModelsLibrary.Models.ViewModels;

namespace Ultra77Razor.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly MssqlContext _contect;

		public IndexModel(ILogger<IndexModel> logger, MssqlContext contect)
		{
			_logger = logger;
			_contect = contect;
		}
		[BindProperty]
		public List<Product> ProductList { get; set; }
		[BindProperty]
		public List<Category> CategoryList { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			ProductList =await _contect.Products.ToListAsync();
			CategoryList =await _contect.Categories.ToListAsync();
			return Page();
		}
	}
}