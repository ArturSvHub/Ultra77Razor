using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Data;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Category
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class DeleteModel : PageModel
    {
        private readonly MssqlContext _context;
		private readonly IWebHostEnvironment _env;

        public DeleteModel(MssqlContext context, IWebHostEnvironment env)
        {
            this._context = context;
			this._env = env;
        }
		[BindProperty]
		public UpakModelsLibrary.Models.Category Category { get; set; }
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			else
			{
				UpakModelsLibrary.Models.Category? category = await _context.Categories.FindAsync(id);
				if (category == null)
				{
					return NotFound();
				}
				return Page();
			}

		}
		public async Task<IActionResult> OnPostAsync(int? id)
		{
			
			var objFromDb = await _context.Categories.FindAsync(id.GetValueOrDefault());
            var path = Path.Combine(_env.WebRootPath, "img", "categories",objFromDb.Name);
            if (objFromDb== null)
			{
				return NotFound();
			}
			if(Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
			_context.Categories.Remove(objFromDb);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
