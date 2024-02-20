using DiabetesDiaryPages.Data;
using DiabetesDiaryPages.Helpers;
using DiabetesDiaryPages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace DiabetesDiaryPages.Pages.Inventory
{
    public class AddItemModel : PageModel
    {
        private readonly DiabetesDiaryDbContext _diabetesDiaryDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public AddItemModel(DiabetesDiaryDbContext diabetesDiaryDbContext, UserManager<IdentityUser> userManager)
        {
            _diabetesDiaryDbContext = diabetesDiaryDbContext;
            _userManager = userManager;
        }

        public List<SelectListItem> MedicalItemSelectListItems { get; set; }
        public List<SelectListItem> MedicalTypeSelectListItems { get; set; }

        [BindProperty]
        public MedicalItem Item { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateSelectList();

            return Page();
        }

        [NonHandler]
        private async Task PopulateSelectList()
        {
            var medicalItemTypes = await _diabetesDiaryDbContext.Medikament
                .Select(e => e.TipMedikament)
                .ToListAsync();

            MedicalTypeSelectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Value = MedicalItemTypes.Insulin.ToString(), Text = "Insulin"},
                new SelectListItem(){Value = MedicalItemTypes.BloodMeasurementStrip.ToString(), Text = "Blood measurement strips"}
            };

            MedicalItemSelectListItems = new List<SelectListItem>();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                await PopulateSelectList();

                return Page();
            }

            var user = User.Identity;

            if (user == null)
            {
                return Challenge();
            }
            var userName = user.Name;

            if (userName == null)
            {
                return BadRequest();
            }

            var identity = await _userManager.FindByNameAsync(userName);
            if (identity == null) 
            {
                return Challenge();    
            }
            var email = await _userManager.GetEmailAsync(identity);

            int dataDbUserId = await _diabetesDiaryDbContext.Covek
                .Where(e => e.Email.Equals(email) && e.Dijabeticar != null)
                .Select(e => e.Id)
                .FirstAsync();

            var inventoryItem = await _diabetesDiaryDbContext.Inventar
                .FindAsync(dataDbUserId, Item.MedicalItemId, DateOnly.FromDateTime(Item.ExpirationDate));

            if (inventoryItem == null)
            {
                Inventar newInventoryItem = new()
                {
                    DijabeticarId = dataDbUserId,
                    MedikamentId = Item.MedicalItemId,
                    RokNaTraenje = DateOnly.FromDateTime(Item.ExpirationDate),
                    Kolicina = Item.Quantity
                };
                _diabetesDiaryDbContext.Inventar.Add(newInventoryItem);
                await _diabetesDiaryDbContext.SaveChangesAsync();
            }
            else
            {
                inventoryItem.Kolicina += Item.Quantity;
                _diabetesDiaryDbContext.Update(inventoryItem);
                await _diabetesDiaryDbContext.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }

        public async Task<JsonResult> OnGetInsulinItemsAsync()
        {
            var data = await _diabetesDiaryDbContext.Insulin
                .Select(e => new
                {
                    e.Id,
                    Type = e.TipInsulin.Ime,
                    Name = e.Medikament.Ime,
                    Manufacturer = e.Medikament.Proizvoditel
                })
                .ToListAsync();

            return new JsonResult(data);
        }

        public async Task<JsonResult> OnGetBloodMeasurementStripsAsync()
        {
            var data = await _diabetesDiaryDbContext.LentiMerenjeShekjer
                .Select(e => new
                {
                    e.Id,
                    Name = e.Medikament.Ime,
                    Manufacturer = e.Medikament.Proizvoditel
                })
                .ToListAsync();

            return new JsonResult(data);
        }

        public class MedicalItem
        {
            public MedicalItemTypes MedicalItemType { get; set; }

            public int MedicalItemId { get; set; }

            [DataType(DataType.Date)]
            public DateTime ExpirationDate { get; set; }

            public int Quantity { get; set; }
        }
    }
}
