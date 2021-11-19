using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class InstructionalStep
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }

        public string Content { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
