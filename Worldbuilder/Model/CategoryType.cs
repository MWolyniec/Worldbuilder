using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder.Model
{
    public class CategoryType
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
