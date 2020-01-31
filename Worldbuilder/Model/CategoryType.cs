using System.Collections.Generic;
using Worldbuilder.Model.BaseClasses;

namespace Worldbuilder.Model
{
    public class CategoryType : IdName
    {

        public IList<Category> Categories { get; set; }
    }
}
