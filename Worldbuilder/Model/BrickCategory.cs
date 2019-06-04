using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder.Model
{
    public class BrickCategory
    {
        public int BrickId { get; set; }
        public Brick Brick { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
