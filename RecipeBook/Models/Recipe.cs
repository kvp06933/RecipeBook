using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string RBUserId { get; set; }

        //Descriptive properties
        [Required]
        [StringLength(100, ErrorMessage = "Your {0} must be at least {2} characters and at most {1}", MinimumLength = 2)]
        public string Title { get; set; }

        //A property to get the user interested without forcing them to read entire post
        [Required]
        [StringLength(500, ErrorMessage = "Your {0} must be at least {2} characters and at most {1}", MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Updated Date")]
        public DateTime? Updated { get; set; }

        //Image fields - IFormFile, Image Data, Image Type

        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        //Navigational properties
        //parent
        public virtual Book Book { get; set; }
        public virtual RBUser RBUser { get; set; }
        //Children
        public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
        public ICollection<InstructionalStep> InstructionalSteps { get; set; } = new HashSet<InstructionalStep>();


    }
}
