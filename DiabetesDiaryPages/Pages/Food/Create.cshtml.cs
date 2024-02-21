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
using Microsoft.AspNetCore.Identity;

namespace DiabetesDiaryPages.Pages.Food
{
    public class CreateModel : PageModel
    {
        private readonly DiabetesDiaryPages.Data.DiabetesDiaryDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateModel(DiabetesDiaryPages.Data.DiabetesDiaryDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {

            await PopulateSelectList();

            return Page();
        }

        public List<SelectListItem> PreparationTypesSelectList { get; set; }

        [BindProperty]
        public Food Item { get; set; }

        [NonHandler]
        public async Task PopulateSelectList()
        {
            PreparationTypesSelectList = await _context
                .NacinPrigotvuvanjeHrana
                .Select(e => new SelectListItem() { Value = e.Id.ToString(), Text = e.Ime })
                .ToListAsync();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            var newFoodItem = new Hrana
            {
                Ime = Item.Name,
                Proizvoditel = Item.Manufacturer,
                GlikemiskiIndeks = Item.GlicemicIndex,
                Kalorii = Item.Calories,
                Jaglehidrati = Item.Carbohydrates,
                Shekjeri = Item.Sugars,
                Masti = Item.Fat,
                Proteini = Item.Proteins,
                NacinPrigotvuvanjeHranaId = Item.PreparationType
            };

            await _context.AddAsync(newFoodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public class Food
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Manufacturer { get; set; }
            [Required]
            [Range(0,10)]
            public int GlicemicIndex { get; set; }
            [Required]
            [Range(0, 99)]
            public decimal Proteins { get; set; }
            [Required]
            [Range(0, 99)]
            public decimal Carbohydrates { get; set; }
            [Required]
            [Range(0, 99)]
            public decimal Sugars { get; set; }
            [Required]
            [Range(0, 99)]
            public decimal Fat { get; set; }
            [Required]
            public int Calories { get; set; }
            [Required]
            public int PreparationType { get; set; }
        }
    }
}
