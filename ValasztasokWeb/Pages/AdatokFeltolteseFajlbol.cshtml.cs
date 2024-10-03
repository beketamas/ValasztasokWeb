using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValasztasokWeb.Models;

namespace ValasztasokWeb.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
    {
        public IWebHostEnvironment _env { get; set; }
        public ValasztasDbContext _con { get; set; }
        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, ValasztasDbContext con)
        {
            _env = env;
            _con = con;
        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }
        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            var UploadFilePath = Path.Combine(_env.ContentRootPath, "Uploads",
                UploadFile.FileName);

            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            StreamReader sr = new(UploadFilePath);
			List<Part> p = _con.Partok.ToList();
			sr = new(UploadFilePath);
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor?.Split();
                Jelolt ujJelolt = new();
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.SzavazatokSzama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3];
                ujJelolt.PartRovidNev = elemek[4];
                if (!p.Select(x => x.RovidNev).Contains(elemek[4]))
                {
                    p.Add(new Part {RovidNev = elemek[4] });
                    _con.Partok.Add(new Part { RovidNev = elemek[4] });
                }
                _con.JeloltekListaja.Add(ujJelolt);

            }
            sr.Close();
            _con.SaveChanges();
            return Page();
        }
    }
}
