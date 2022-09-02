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
    public class EditModel : PageModel
    {
        private readonly MssqlContext _context;

		public EditModel(MssqlContext context)
		{
			_context = context;
		}
		[BindProperty]
		public UpakModelsLibrary.Models.Category Category { get; set; }
		public async Task<IActionResult> OnGetAsync(int? id)
        {
			if(id==null||id==0)
			{
				return NotFound();
			}
			else
			{
				Category = await _context.Categories.FindAsync(id);
				if(Category==null)
				{
					return NotFound();
				}
				return Page();
			}

        }
        public async Task<IActionResult> OnPostAsync()
		{
			var files = HttpContext.Request.Form.Files;
			if (files.Count > 0)
			{
				Category.Image = await files[0].ImageToImageDataAsync();
			}
			else
			{
				var objFromDb = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(
					x=> x.Id == Category.Id);
				Category.Image = objFromDb.Image;
			}
			_context.Categories.Update(Category);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");

		}
    }
}
