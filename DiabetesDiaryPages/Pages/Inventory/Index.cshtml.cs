using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DiabetesDiaryPages.Data;
using DiabetesDiaryPages.Models;
using System.ComponentModel.DataAnnotations;
using DiabetesDiaryPages.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;

namespace DiabetesDiaryPages.Pages.Inventory
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

        public IList<InventoryDTO> InventoryItems { get;set; } = default!;

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

            IdentityUser? IU = await _userManager.FindByNameAsync(userName);

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

            InventoryItems = await _context
                .Inventar
                .Include(e => e.Medikament)
                .ThenInclude(e => e.LentiMerenjeShekjer)
                .Include(e => e.Medikament.Insulin.TipInsulin)
                .Where(e => e.DijabeticarId == dataDbUserId)
                .Select(e => new InventoryDTO
                {
                    MedicalItemId = e.Medikament.Id,
                    MedicalItemType = (MedicalItemTypes)Convert.ToUInt32(e.Medikament.TipMedikament),
                    MedicalItemName = e.Medikament.Ime,
                    MedicalItemManufacturer = e.Medikament.Proizvoditel,
                    InsulinType = e.Medikament.Insulin.TipInsulin.Ime,
                    ExpirationDate = e.RokNaTraenje,
                    Quantity = e.Kolicina
                })
                .ToListAsync();
            return Page();
        }

        public class InventoryDTO
        {
            public int MedicalItemId { get; set; }
            public MedicalItemTypes MedicalItemType { get; set; }
            public string MedicalItemName { get; set; }
            public string MedicalItemManufacturer { get; set; }
            public string? InsulinType { get; set; }

            public int Quantity { get; set; }

            public DateOnly ExpirationDate { get; set; }
        }
    }
}
