using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Product
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class UpsertModel : PageModel
    {
        private readonly MssqlContext _context;
        private readonly IWebHostEnvironment _environment;

        public UpsertModel(MssqlContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [BindProperty]
        public UpakModelsLibrary.Models.Product? Product { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem>? CategorySelectedList { get; set; }
        [BindProperty]
        public List<FileInfo>? Files { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, UpakModelsLibrary.Models.Product? product = null)
        {
            Product = new UpakModelsLibrary.Models.Product();
            CategorySelectedList = _context.Categories?.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            if (id == null)
            {
                return Page();
            }
            else
            {
                //edit
                Product = await _context.Products.FindAsync(id);

                if (Product == null)
                {
                    return NotFound();
                }
                string dirPath = Path.Combine(_environment.WebRootPath, "img", "products", Product.Name);

                Files = new DirectoryInfo(dirPath).GetFiles().ToList();
                product = Product;
                return Page();
            }

        }
        public async Task<IActionResult> OnPostAsync(UpakModelsLibrary.Models.Product? product)
        {
            var files = HttpContext.Request.Form.Files;
            var dirPath = Path.Combine(_environment.WebRootPath, "img", "products", Product.Name);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (product.Id == 0)
            {
                foreach (var file in files)
                {
                    var path = Path.Combine(dirPath, file.FileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                await _context.AddAsync(product);
            }
            else
            {

                var currentDirectory = new DirectoryInfo(dirPath);

                var objFromDb = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(
                        x => x.Id == Product.Id);
                if (objFromDb != null)
                {
                    var oldDirPath = Path.Combine(_environment.WebRootPath, "img", "products", objFromDb.Name);
                    var oldDirectory = new DirectoryInfo(oldDirPath);

                    if (currentDirectory.Name != oldDirectory.Name || !currentDirectory.Exists)
                    {
                        if (Directory.Exists(dirPath))
                            Directory.Delete(dirPath, true);
                        oldDirectory.MoveTo(dirPath);
                    }
                }

                else
                {

                    foreach (var file in files)
                    {
                        var path = Path.Combine(dirPath, file.FileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
                _context.Update(product);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("Upsert", new { id = Product.Id });
        }
        public IActionResult OnPostDelete(string imageName)
        {
            if (System.IO.File.Exists(Path.Combine(_environment.WebRootPath, "img", "products", Product.Name, imageName)))
            {
                System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "img", "products", Product.Name, imageName));
            }
            return RedirectToPage("Index");
        }
    }
}
//edit
//if (files.Count!=0)
//{
//	product.Image = await files[0].ImageToImageDataAsync();
//}
//else
//{
//	var objFromDb = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
//	product.Image = objFromDb?.Image;
//}