using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Worldbuilder.Model.BaseClasses;

namespace Worldbuilder.Model
{
    public class Category : IdName
    {


        [Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Display(Name = "Bricks")]
        public IList<BrickCategory> BrickCategories { get; set; }

        [Display(Name = "Type")]
        public CategoryType CategoryType { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
