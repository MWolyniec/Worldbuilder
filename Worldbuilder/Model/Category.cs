using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder.Model
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

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
