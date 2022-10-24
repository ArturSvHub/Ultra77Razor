using Microsoft.AspNetCore.Mvc;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Pages.Components.CategoryView
{
    public class CategoryView : ViewComponent
    {
        public List<FileInfo> Files { get; set; }
        public CategoryView()
        {

        }
        public IViewComponentResult Invoke(CategoryViewModel categoryView)
        {
            return View("Default",categoryView);
        }
    }
    public class CategoryViewModel
    {
        public Category? Category { get; set; }
        public List<FileInfo>? Files { get; set; }
    }
}
