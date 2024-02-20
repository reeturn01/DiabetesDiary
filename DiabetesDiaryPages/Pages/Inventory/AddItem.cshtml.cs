using DiabetesDiaryPages.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public SelectList MedicalSelectListItems { get; set; }

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
            SelectListGroup bloodMeasurementStripsSelectListGroup = new SelectListGroup() { Name = "Blood measurement strips" };
            SelectListGroup insulinsSelectListGroup = new SelectListGroup() { Name = "Insulins" };

            var measurementStripsData = await _diabetesDiaryDbContext
                .LentiMerenjeShekjer
                .Select(e => new
                {
                    e.Id,
                    Name = string.Join(", ", e.Medikament.Proizvoditel, e.Medikament.Ime),
                    Type = "Blood measurement strips"
                }).ToListAsync();

            var insulinsData = await _diabetesDiaryDbContext
                .Insulin
                .Select(e => new
                {
                    e.Id,
                    Name = string.Join(", ", e.TipInsulin.Ime, e.Medikament.Proizvoditel, e.Medikament.Ime),
                    Type = "Insulin"
                })
                .ToListAsync();

            //var medicalSelectListItems = measurementStripsData
            //    .Select(e => new SelectListItem()
            //    {
            //        Value = e.Id.ToString(),
            //        Text = string.Join(", ", e.Proizvoditel, e.Ime),
            //        Group = bloodMeasurementStripsSelectListGroup
            //    });


            //var selectListItems = insulinsData
            //    .Select(e => new SelectListItem()
            //    {
            //        Value = e.Id.ToString(),
            //        Text = string.Join(", ", e.TipInsulin, e.Proizvoditel, e.Ime),
            //        Group = insulinsSelectListGroup
            //    })
            //    .Concat(medicalSelectListItems)
            //    .ToList();

            measurementStripsData.AddRange(insulinsData);

            MedicalSelectListItems = new SelectList(measurementStripsData, "Id", "Name", null, "Type");

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                await PopulateSelectList();

                //Do something because wrong!?
                return Page();
            }

            return RedirectToPage("/Index");
        }

        public class MedicalItem
        {
            public int MedicalItemId { get; set; }

            [DataType(DataType.Date)]
            public DateTime ExpirationDate { get; set; }

            public int Quantity { get; set; }
        }
    }
}
