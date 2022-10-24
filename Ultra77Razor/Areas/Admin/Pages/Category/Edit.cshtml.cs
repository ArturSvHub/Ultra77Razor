using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Data;
using System.Xml.Linq;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Category
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class EditModel : PageModel
    {
        private readonly MssqlContext _context;
		private readonly IWebHostEnvironment _env;

		public EditModel(MssqlContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}
		[BindProperty]
		public UpakModelsLibrary.Models.Category Category { get; set; }
        [BindProperty]
        public List<FileInfo>? Files { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
			if(id==null||id==0)
			{
				return NotFound();
			}
			else
			{
				Category = await _context.Categories.FindAsync(id);
                var dirPath = Path.Combine(_env.WebRootPath, "img", "categories", Category.Name);
				Files = new DirectoryInfo(dirPath).GetFiles().ToList();
                if (Category==null)
				{
					return NotFound();
				}
				return Page();
			}

        }
        public async Task<IActionResult> OnPostAsync()
		{
			var files = HttpContext.Request.Form.Files;
            var dirPath = Path.Combine(_env.WebRootPath, "img", "categories", Category.Name);
			var currentDirectory = new DirectoryInfo(dirPath);
			
            var objFromDb = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(
                    x => x.Id == Category.Id);

			var oldDirPath = Path.Combine(_env.WebRootPath, "img", "categories", objFromDb.Name);
            var oldDirectory = new DirectoryInfo(oldDirPath);

            if (currentDirectory.Name!=oldDirectory.Name||!currentDirectory.Exists)
			{
				//Category.Image = await files[0].ImageToImageDataAsync();
				oldDirectory.MoveTo(dirPath);
			}
			else
			{
				
				Category.Image = objFromDb.Image;
			}
			_context.Categories.Update(Category);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");

		}
		public async Task<IActionResult> OnPostDeleteAsync(string imageName)
		{
            if (System.IO.File.Exists(Path.Combine(_env.WebRootPath, "img","categories",Category.Name, imageName)))
			{
                System.IO.File.Delete(Path.Combine(_env.WebRootPath, "img", "categories", Category.Name, imageName));
            }
            return RedirectToPage("Edit", new {id=Category.Id});
        }
    }
}
