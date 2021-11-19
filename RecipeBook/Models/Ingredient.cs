using Microsoft.AspNetCore.Http;
using RecipeBook.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public Measurement Measurement { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
