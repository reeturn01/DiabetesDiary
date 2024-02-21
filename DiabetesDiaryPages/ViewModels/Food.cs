using System.ComponentModel.DataAnnotations;

namespace DiabetesDiaryPages.ViewModels
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        [Range(0, 10)]
        public int GlicemicIndex { get; set; }
        [Required]
        [Range(0, 99)]
        public int Proteins { get; set; }
        [Required]
        [Range(0, 99)]
        public int Carbohydrates { get; set; }
        [Required]
        [Range(0, 99)]
        public int Sugars { get; set; }
        [Required]
        [Range(0, 99)]
        public int Fat { get; set; }
        [Required]
        [Range(0, 99)]
        public int Calories { get; set; }
        public string PrepType { get; set; }
    }
}
