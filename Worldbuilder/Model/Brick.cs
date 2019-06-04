using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder.Model
{
    public class Brick
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public IList<BrickCategory> BrickCategories { get; set; }

        [StringLength(100), Display(Name = "Short summary"), Required]
        public string ShortDesc { get; set; }

        [Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string LongDesc { get; set; }

       //[Display(Name = "Contains")]
        public IList<BrickToBrick> Children { get; set; }
        public IList<BrickToBrick> Parents { get; set; }

    }
}
