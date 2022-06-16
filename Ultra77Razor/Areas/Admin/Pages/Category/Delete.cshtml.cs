using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly MssqlContext _context;

        public DeleteModel(MssqlContext context)
        {
            this._context = context;
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
			if(objFromDb== null)
			{
				return NotFound();
			}
			_context.Categories.Remove(objFromDb);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
