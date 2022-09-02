using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UpakModelsLibrary.Models;

using UpakDataAccessLibrary.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.Category
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class IndexModel : PageModel
    {
		private readonly MssqlContext _context;

		public IndexModel(MssqlContext context)
		{
			_context = context;
		}
		[BindProperty]
		public List<UpakModelsLibrary.Models.Category> CatList { get; set; }


		public async Task<IActionResult> OnGetAsync()
        {
			CatList =await _context.Categories.ToListAsync();
			return Page();
		}

    }
}
