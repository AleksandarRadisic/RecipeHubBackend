using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Model.Enumerations;

namespace RecipeHub.ClassLib.Model
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CaloriesPerUnit { get; set; }
        public MeasureUnit MeasureUnit { get; set; }

    }
}
