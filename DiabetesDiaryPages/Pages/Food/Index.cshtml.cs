using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DiabetesDiaryPages.Data;

namespace DiabetesDiaryPages.Pages.Food
{
    public class IndexModel : PageModel
    {
        private readonly DiabetesDiaryPages.Data.DiabetesDiaryDbContext _context;

        public IndexModel(DiabetesDiaryPages.Data.DiabetesDiaryDbContext context)
        {
            _context = context;
        }

        public IList<DiabetesDiaryPages.ViewModels.Food> Food { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Food = await _context
                .Hrana
                .Select(e => new DiabetesDiaryPages.ViewModels.Food
                {
                    Id = e.Id,
                    Name = e.Ime,
                    Manufacturer = e.Proizvoditel,
                    Calories = e.Kalorii,
                    Carbohydrates = Convert.ToInt32(e.Jaglehidrati),
                    Sugars = Convert.ToInt32(e.Shekjeri),
                    Fat = Convert.ToInt32(e.Masti),
                    Proteins = Convert.ToInt32(e.Proteini),
                    GlicemicIndex = e.GlikemiskiIndeks,
                    PrepType = e.NacinPrigotvuvanjeHrana.Ime
                })
                .ToListAsync();
        }
    }
}
