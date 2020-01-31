using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldbuilder.Model;

namespace Worldbuilder.Data
{
    public class CategoryTypeBuilder
    {
        //TO jest gowno , ale PoC
        private IList<CategoryType> Categories;

        public IList<CategoryType> Build(string[] categories)
        {
            foreach(var name in categories)
            {
                Categories.Add( 
                    new CategoryType() { Name = name }
                    );
            }

            return Categories;
        }

        public CategoryTypeBuilder()
        {
            Categories = new List<CategoryType>();
        }
    }
}
