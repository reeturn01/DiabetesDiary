using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DiabetesDiaryPages.Data;
using DiabetesDiaryPages.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace DiabetesDiaryPages.Pages.Records
{
    public class CreateModel : PageModel
    {
        private readonly DiabetesDiaryPages.Data.DiabetesDiaryDbContext _context;

        public CreateModel(DiabetesDiaryPages.Data.DiabetesDiaryDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DijabeticarId"] = new SelectList(_context.Dijabeticar, "Id", "Id");
            var foodList = new List<SelectListItem> { new() { Text = "-- Izberi Proizvoditel --", Disabled = true, Selected = true } };

            var foodData = _context.Hrana
                .Select(item => new
                {
                    FoodName = string.Join(", ", item.Ime, item.Proizvoditel, item.NacinPrigotvuvanjeHrana.Ime),
                    Id = item.Id
                })
                .ToList();
            foodData
                .ForEach(item => foodList.Add(new SelectListItem
                {
                    Text = item.FoodName,
                    Value = item.Id.ToString()
                }));
            ViewData["FoodSelectItemList"] = foodList;

            return Page();
        }

        [BindProperty]
        public RecordModel Record { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetFoodDetails(int id)
        {
            var FoodDetail = await _context.Hrana.Include(e => e.NacinPrigotvuvanjeHrana)
                .Where(e => e.Id == id)
                .Select(item => new
                {
                    item.Id,
                    Name = item.Ime,
                    Manufacturer = item.Proizvoditel,
                    PreparationType = item.NacinPrigotvuvanjeHrana.Ime,
                    Calories = item.Kalorii,
                    Proteins = item.Proteini,
                    Fat = item.Masti,
                    Carbohydrates = item.Jaglehidrati,
                    Sugars = item.Shekjeri,
                    GlicemicIndex = item.GlikemiskiIndeks,
                })
                .FirstOrDefaultAsync();

            if (FoodDetail == null)
            {
                return BadRequest();
            }
            return new JsonResult(JsonConvert.SerializeObject(FoodDetail));

        }

        public async Task<IActionResult> OnGetGenerateFoodInputFields()
        {
            ViewData["FoodSelectListItems"] = _context.Hrana
                .Select(f => new
                {
                    Id = f.Id,
                    Name = string.Join(", ", f.Proizvoditel, f.Ime)
                })
                .ToList()
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                })
                .ToList();
            return Partial("_GenerateFoodInputFields", new FoodModel());
        }

    }
    public class RecordModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool HasMeasurementRecord { get; set; }
        public MeasurementRecordModel MeasurementRecord { get; set; }
        public bool HasMealRecord { get; set; }
        public MealRecordModel MealRecord { get; set; }
        public bool HasInsulinRecord { get; set; }
        public InsulinRecordModel InsulinRecord { get; set; }
    }

    public class MeasurementRecordModel
    {
        public decimal Measurement { get; set; }
    }

    public class MealRecordModel
    {
        public List<FoodModel> Food { get; set;} 
    }

    public class FoodModel
    {
        public int FoodId { get; set; }
        public int Amount { get; set; }
    }

    public class InsulinRecordModel
    {
        public List<InsulinAdministrationModel> InsulinAdministrations { get; set; }
    }

    public class InsulinAdministrationModel
    {
        public int InsulinId { get; set; }
        public int Amount { get; set; }
    }
}
