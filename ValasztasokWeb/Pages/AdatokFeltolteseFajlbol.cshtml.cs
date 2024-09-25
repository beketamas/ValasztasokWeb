using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValasztasokWeb.Models;

namespace ValasztasokWeb.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
    {
        public IWebHostEnvironment _env { get; set; }
        public ValasztasDbContext _con { get; set; }
        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, ValasztasDbContext con, IFormFile uploadFile)
        {
            _env = env;
            _con = con;
            UploadFile = uploadFile;
        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var UploadFilePath = Path.Combine(_env.ContentRootPath, "Uploads",
                UploadFile.FileName);

            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            StreamReader sr = new(UploadFilePath);
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor?.Split();
                Jelolt ujJelolt = new();
                Part ujPart = new();
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.SzavazatokSzama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3];
                ujPart.RovidNev = elemek[4];
                ujJelolt.Part = ujPart;
                _con.JeloltekListaja.Add(ujJelolt);
                _con.Partok.Add(ujPart);
            }
            sr.Close();
            _con.SaveChanges();
            return Page();
        }
    }
}
