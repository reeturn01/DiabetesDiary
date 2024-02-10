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

        public class RecordModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
            public MeasurementRecordModel MeasurementRecord { get; set; }
            public MealRecordModel MealRecord { get; set; }
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
}
