using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Worldbuilder.Model.BaseClasses;

namespace Worldbuilder.Model
{
    public class Brick : IdName
    {

        [Display(Name = "Category")]
        public IList<BrickCategory> BrickCategories { get; set; }

        [StringLength(250), Display(Name = "Short summary"), Required]
        public string ShortDesc { get; set; }

        [Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string LongDesc { get; set; }


        public IList<BrickToBrick> Children { get; set; }
        public IList<BrickToBrick> Parents { get; set; }

    }
}
