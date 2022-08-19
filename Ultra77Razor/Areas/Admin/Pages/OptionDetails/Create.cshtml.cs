using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Areas.Admin.Pages.OptionDetails
{
    public class CreateModel : PageModel
    {
        private readonly MssqlContext _context;

        public CreateModel(MssqlContext mssqlContext)
        {
            _context = mssqlContext;
        }

        [BindProperty]
        public OptionDetail Detail { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem>? OptionSelectedList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Detail = new OptionDetail();
            OptionSelectedList = _context.ProductOptions?.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _context.AddAsync(Detail);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/Options/Index");
        }
    }
}
