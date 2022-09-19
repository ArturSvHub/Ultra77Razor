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
        public string NameMainPagesFile { get; set; }
        private readonly MssqlContext context;

        public MainPagesModel(MssqlContext context)
        {
            this.context = context;
        }
        public async Task OnGetAsync()
        {
            var options = new JsonSerializerOptions
            {
                Encoder=JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,UnicodeRanges.Cyrillic),
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
                System.IO.File.Create(NameMainPagesFile).Close();
                string json = JsonSerializer.Serialize(MainPages,options);
                System.IO.File.WriteAllTextAsync(NameMainPagesFile, json, Encoding.UTF8);
            }
            else
            {
                using FileStream stream = System.IO.File.OpenRead(NameMainPagesFile);
                MainPages = await JsonSerializer.DeserializeAsync<List<MainPage>>(stream);
                await stream.DisposeAsync();
            }

        }
        public async Task OnPostAsync()
        {

        }
    }
}
