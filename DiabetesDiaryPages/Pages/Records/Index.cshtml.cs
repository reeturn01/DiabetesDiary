using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DiabetesDiaryPages.Data;
using DiabetesDiaryPages.Models;
using Microsoft.AspNetCore.Identity;

namespace DiabetesDiaryPages.Pages.Records
{
    public class IndexModel : PageModel
    {
        private readonly DiabetesDiaryPages.Data.DiabetesDiaryDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(DiabetesDiaryPages.Data.DiabetesDiaryDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<RecordDTO> Records { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdentity = User.Identity;
            if (userIdentity == null)
            {
                return Challenge();
            }

            var userName = userIdentity.Name;
            if (userName == null)
            {
                return Forbid();
            }

            var IU = await _userManager.FindByNameAsync(userName);
            if (IU == null)
            {
                return Challenge();
            }

            var userEmail = await _userManager.GetEmailAsync(IU);
            if (userEmail == null)
            {
                return Forbid();
            }

            var dataDbUserId = await _context
                .Covek
                .Where(e => e.Email.Equals(userEmail) && e.Dijabeticar != null)
                .Select(e => e.Id)
                .FirstAsync();
            Records = await _context
                .Zapis
                .Where(e => e.DijabeticarId == dataDbUserId)
                .Select(e => new RecordDTO
                {
                    Date = e.Data,
                    BloodMeasurementValue = e.ZapisSoIzmerenSekjer.Vrednost,
                    FoodItems = e.ZapisHrana.Obrok
                    .Select(f => new Food()
                    {
                        Name = string.Join(", ", f.Hrana.Proizvoditel, f.Hrana.Ime, f.Hrana.NacinPrigotvuvanjeHrana.Ime),
                        Amount = f.Kolicina
                    })
                    .ToList(),
                    InsulinAdministrations = e.ZapisInsulin.ZapisInsulinDoziranInsulin
                    .Select(ia => new InsulinAdministration
                    {
                        Name = string.Join(", ", ia.Insulin.TipInsulin.Ime, ia.Insulin.Medikament.Proizvoditel, ia.Insulin.Medikament.Ime),
                        Amount = ia.Kolicina
                    })
                    .ToList()
                })
                .ToListAsync();

            return Page();
        }

        public class RecordDTO
        {
            public DateOnly Date { get; set; }
            public decimal? BloodMeasurementValue { get; set; }
            public List<Food>? FoodItems { get; set; }
            public List<InsulinAdministration>? InsulinAdministrations { get; set; }
        }

        public class Food
        {
            public string Name { get; set; } = string.Empty;
            public int Amount { get; set; }
        }
        public class InsulinAdministration
        {
            public string Name { get; set; } = string.Empty;
            public int Amount { get; set; }
        }
    }
}
