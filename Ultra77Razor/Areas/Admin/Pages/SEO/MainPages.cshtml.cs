using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models.SEO;

using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.SEO
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class MainPagesModel : PageModel
    {
        [BindProperty]
        public List<MainPage> MainPages { get; set; }
        public string NameMainPagesFile { get; set; } = "MainPages.json";
        private readonly MssqlContext context;

        public MainPagesModel(MssqlContext context)
        {
            this.context = context;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            NameMainPagesFile = "MainPages.json";
            if (!System.IO.File.Exists(NameMainPagesFile))
            {
                MainPages = new List<MainPage>()
                {
                    new MainPage()
                    {
                        Id=1,
                        Title="�������",
                        Description="����������� ��������� ����� � ������",
                        KeyWords="��������, �����������, �����������, ����, ���, �������, �������, ������, �����������, �����, ������,������,�����, �����,������,�����, ��������, �����,��,��, upak, ����������, �������, ��������, ��������, ��������, ������������� �����, ����������� �����,�����, �����, �����,��������, ������, ���������"
                    },
                    new MainPage()
                    {
                        Id=2,
                        Title="������",
                        Description="",
                        KeyWords=""
                    },new MainPage()
                    {
                        Id=3,
                        Title="�����������",
                        Description="",
                        KeyWords=""
                    },
                    new MainPage()
                    {
                        Id=4,
                        Title="� ��������",
                        Description="",
                        KeyWords=""
                    },
                    new MainPage()
                    {
                        Id=5,
                        Title="��������",
                        Description="",
                        KeyWords=""
                    }
                };
                WriteJsonToFile(MainPages, NameMainPagesFile, options);
            }
            else
            {
                MainPages = await ReadJsonFromFile(NameMainPagesFile);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            await WriteJsonToFile(MainPages, NameMainPagesFile, options);
            return Page();
        }
        public async Task OnPostDeleteAsync([FromForm] int id)
        {

        }
        private async Task<List<MainPage>> ReadJsonFromFile(string path)
        {
            using FileStream stream = System.IO.File.OpenRead(NameMainPagesFile);
            return await JsonSerializer.DeserializeAsync<List<MainPage>>(stream);
            await stream.DisposeAsync();
        }
        private async Task WriteJsonToFile(List<MainPage> body, string path, JsonSerializerOptions? options = null)
        {
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path).Close();
                }
                string json = JsonSerializer.Serialize(body, options);
                await System.IO.File.WriteAllTextAsync(path, json, Encoding.UTF8);
        }
    }
}
