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
using Newtonsoft.Json;
using DiabetesDiaryPages.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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

            FoodSelectItems = [new SelectListItem { Text = "-- Select Food item --", Selected = true, Disabled = true }];
            InsulinSelectItems = [new SelectListItem { Text = "-- Select Insulin item --", Selected = true, Disabled = true }];

            _context.Hrana.Include(e => e.NacinPrigotvuvanjeHrana)
                .ForEachAsync(e => FoodSelectItems.Add(new SelectListItem { Text = string.Join(", ", e.Proizvoditel, e.Ime, e.NacinPrigotvuvanjeHrana.Ime), Value = e.Id.ToString() })).Wait();

            _context.Insulin.Include(e => e.Medikament).Include(e => e.TipInsulin)
                .ForEachAsync(e => InsulinSelectItems.Add(new SelectListItem { Text = string.Join(", ", e.Medikament.Proizvoditel, e.Medikament.Ime, e.TipInsulin.Ime), Value = e.Id.ToString() })).Wait();

            Record = new()
            {
                Date = new DateTime(2011, 09, 28),
                HasBloodMeasurement = true,
                BloodMeasurementRecord = new() { Measurement = 9.7M },
                HasInsulin = true,
                InsulinRecord = new InsulinRecordModel
                {
                    InsulinAdministrations = new List<InsulinAdministrationModel>(){
                        new InsulinAdministrationModel()
                        {
                            InsulinId = 1,
                            Amount = 10
                        },
                        new InsulinAdministrationModel()
                        {
                            InsulinId = 2,
                            Amount = 20
                        }
                    }
                },
                HasMeal = true,
                MealRecord = new()
                {
                    Food = new List<FoodModel>
                    {
                        new FoodModel()
                        {
                            FoodId = 1,
                            Amount = 10
                        },
                        new FoodModel()
                        {
                            FoodId= 2,
                            Amount = 20
                        },
                        new FoodModel()
                        {
                            FoodId = 3,
                            Amount = 30
                        }
                    }
                }
            };

            return Page();
        }

        [BindProperty]
        public RecordModel Record { get; set; }
        public List<SelectListItem> FoodSelectItems { get; set; }
        public List<SelectListItem> InsulinSelectItems { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid
                || (!Record.HasBloodMeasurement && !Record.HasInsulin && !Record.HasMeal)
                )
            {
                // Nesto ne e vo red so modelot, prikazi povtorno strana

                FoodSelectItems = [new SelectListItem { Text = "-- Select Food item --", Selected = true, Disabled = true }];
                InsulinSelectItems = [new SelectListItem { Text = "-- Select Insulin item --", Selected = true, Disabled = true }];

                _context.Hrana.Include(e => e.NacinPrigotvuvanjeHrana)
                    .ForEachAsync(e => FoodSelectItems.Add(new SelectListItem { Text = string.Join(", ", e.Proizvoditel, e.Ime, e.NacinPrigotvuvanjeHrana.Ime), Value = e.Id.ToString() })).Wait();

                _context.Insulin.Include(e => e.Medikament).Include(e => e.TipInsulin)
                    .ForEachAsync(e => InsulinSelectItems.Add(new SelectListItem { Text = string.Join(", ", e.Medikament.Proizvoditel, e.Medikament.Ime, e.TipInsulin.Ime), Value = e.Id.ToString() })).Wait();


                return Page();
            }

            if (User.Identity == null)
            {
                return Unauthorized();
            }
            if (User.Identity.Name.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            // DA SE PREPRAVI
            string? currentUserName = User.Identity.Name;
            var dijabeticarId = await _context.Covek
                .Where(e => e.Email == "nikola.torbovski@students.finki.ukim.mk" && e.Dijabeticar != null)
                .Select(e => e.Id)
                .SingleAsync();


            var newRecord = new Zapis() { 
                DijabeticarId = dijabeticarId,
                Data = DateOnly.FromDateTime(Record.Date)
            };

            if (Record.HasMeal)
            {
                
                Record.MealRecord.Food
                    .ForEach(item => {
                        newRecord.ZapisHrana.Obrok
                            .Add(new Obrok()
                            {
                                HranaId = item.FoodId,
                                Kolicina = item.Amount
                            });
                    });
            }
            if (Record.HasBloodMeasurement) {
                newRecord.ZapisSoIzmerenSekjer.Vrednost = Record.BloodMeasurementRecord.Measurement;
            }
            if (Record.HasInsulin) {
                Record.InsulinRecord.InsulinAdministrations.ForEach(item => {
                    newRecord.ZapisInsulin.ZapisInsulinDoziranInsulin
                    .Add(new ZapisInsulinDoziranInsulin()
                    {
                        InsulinId = item.InsulinId,
                        Kolicina = item.Amount
                    });
                });
            }

            await _context.AddAsync(newRecord, CancellationToken.None);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetFoodItemsAsync()
        {
            
            var FoodItems = _context.Hrana.Include(e => e.NacinPrigotvuvanjeHrana)
                .Select(e => new
                {
                    Name = string.Join(", ", e.Proizvoditel, e.Ime, e.NacinPrigotvuvanjeHrana.Ime),
                    Id = e.Id.ToString(),
                });

            string FoodIdDisplayName = DisplayAnnotationManipulation.GetPropertyDisplayName<FoodModel>(p => p.FoodId);
            string AmountDisplayName = DisplayAnnotationManipulation.GetPropertyDisplayName<FoodModel>(p => p.Amount);

            var output = new
            {
                FoodIdDisplayName,
                AmountDisplayName,
                FoodItems
            };

            return new JsonResult(JsonConvert.SerializeObject(output));
        }

        public async Task<IActionResult> OnGetInsulinItemsAsync()
        {

            var InsulinItems = _context.Insulin.Include(e => e.Medikament).Include(e => e.TipInsulin)
                .Select(e => new
                {
                    Name = string.Join(", ", e.Medikament.Proizvoditel, e.Medikament.Ime, e.TipInsulin.Ime),
                    Id = e.Id.ToString(),
                });

            string InsulinIdDisplayName = DisplayAnnotationManipulation.GetPropertyDisplayName<InsulinAdministrationModel>(p => p.InsulinId);
            string AmountDisplayName = DisplayAnnotationManipulation.GetPropertyDisplayName<InsulinAdministrationModel>(p => p.Amount);

            var output = new
            {
                InsulinIdDisplayName,
                AmountDisplayName,
                InsulinItems
            };

            return new JsonResult(JsonConvert.SerializeObject(output));
        }

        public class RecordModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
            public bool HasBloodMeasurement { get; set; }
            public bool HasMeal { get; set; }
            public bool HasInsulin { get; set; }
            public BloodMeasurementRecordModel? BloodMeasurementRecord { get; set; }
            public MealRecordModel? MealRecord { get; set; }
            public InsulinRecordModel? InsulinRecord { get; set; }
        }

        public class BloodMeasurementRecordModel
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
