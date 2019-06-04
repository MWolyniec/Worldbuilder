using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder.Model
{
    public class BrickToBrick
    {
        public int BrickId { get; set; }
        public Brick Brick { get; set; }

        public int ChildId { get; set; }
        public Brick Child { get; set; }
    }
}
