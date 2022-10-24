using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;
using System.IO;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Category
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class CreateModel : PageModel
	{
		private readonly MssqlContext _contect;
        private readonly IWebHostEnvironment _env;
        public CreateModel(MssqlContext context, IWebHostEnvironment env)
		{
			_contect = context;
			_env = env;
		}
		[BindProperty]
		public UpakModelsLibrary.Models.Category Category { get; set; }
        [BindProperty]
        public List<FileInfo>? Files { get; set; }
        public void OnGet()
		{

        }
		public async Task<IActionResult> OnPostAsync()
		{
			var files = HttpContext.Request.Form.Files;
			//Category.Image = await files[0].ImageToImageDataAsync();
			var dirPath = Path.Combine(_env.WebRootPath, "img", "categories", Category.Name);
            if (!Directory.Exists(dirPath))
			{
                Directory.CreateDirectory(dirPath);
            }
            foreach (var file in files)
			{
                var path = Path.Combine(dirPath,file.FileName);
				
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
			await _contect.Categories.AddAsync(Category);
			await _contect.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
