using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;
using UpakModelsLibrary.Models.SEO;
using UpakModelsLibrary.Models.ViewModels;

namespace Ultra77Razor.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly MssqlContext _contect;
		private readonly IWebHostEnvironment _env;

		public IndexModel(ILogger<IndexModel> logger, MssqlContext contect,IWebHostEnvironment env)
		{
			_logger = logger;
			_contect = contect;
			_env = env;
		}
		[BindProperty]
		public List<Product> ProductList { get; set; }
		[BindProperty]
		public List<Category> CategoryList { get; set; }
		[BindProperty]
		public string BasePath { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
            string? file = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "MainPages.json"));
            List<MainPage> meta = JsonSerializer.Deserialize<List<MainPage>>(file);
            if (meta != null)
            {
                ViewData["Title"] = meta[0].Title;
                ViewData["Description"] = meta[0].Description;
                ViewData["Keywords"] = meta[0].KeyWords;
            }
            ProductList =await _contect.Products.ToListAsync();
			CategoryList =await _contect.Categories.ToListAsync();
			BasePath = Path.Combine(_env.WebRootPath,"img", "categories");
			return Page();
		}
	}
}