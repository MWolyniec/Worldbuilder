using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;  
using NUnit;
using Worldbuilder;


namespace Tests
{
    [TestFixture]
    public class PropertyChecksTests
    {
       

        var noIdObj = new { justName = "name" };

        var onlyIdObj = new { Id = 3 };

        var twoFKIds = new { FirstId = 2, SecondId = 3 };

        var oneOwnIdOneFK = new { Id = 2, OtherId = 1 };

        var namedOwnId = new { namedOwnIdId = 1 };



    [Test]
        public void HasOwnIdTest()
        {
            Assert.AreEqual( (GetType(namedOwnId)).HasOwnId(), true );


        }
    }
}
