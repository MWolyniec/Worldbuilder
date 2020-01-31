using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder.Model
{
    //TODO: [Table(nameof(BrickToBrick))]
    public class BrickToBrick
    {
        //TODO:[Key]
        public int BrickId { get; set; }
        public Brick Brick { get; set; }

        //TODO: To jest w takim razie ForeginKey z klasa atrybuty     [ForeignKey]

        public int ChildId { get; set; }
        public Brick Child { get; set; }
        

       //TODO: To nie zadziała ? Nie znam sie w CORY ::::(((((( , życie to chuj:
        public BrickToBrick(int id,Brick brick)
        {
            BrickId = id;
            Brick = brick;
        }
    }
}
