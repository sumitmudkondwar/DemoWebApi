using DemoWebApi.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Models
{
    public class Shirt
    {
        public int ShirtId { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Color { get; set; }

        [Shirt_EnsureCorrectSizing]
        public int? Size { get; set; }

        [Required]
        public string? Gender { get; set; }
        
        public double price { get; set; }

    }
}
