using System.ComponentModel.DataAnnotations;

namespace Worldbuilder.Model.BaseClasses
{
    public class IdName
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

    }
}
