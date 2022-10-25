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
        private readonly IWebHostEnvironment _env;

        public DetailsModel(MssqlContext context,IWebHostEnvironment env)
		{
			_context = context;
            _env = env;
		}

		[BindProperty]
        public IEnumerable<Product> Products{ get; set; }
		[BindProperty]
        public IEnumerable<Category> Categories { get; set; }
        [BindProperty]
        public string BasePath { get; set; }
        [BindProperty]
        public List<FileInfo> Files { get; set; }
        [BindProperty]        
		public int CurrentId { get; set; }
		public void OnGet(int id)
        {
            CurrentId = id;
            Products = _context.Products.Include(u => u.Category);
            Categories = _context.Categories;
            BasePath = _env.WebRootPath;
        }
    }
}
