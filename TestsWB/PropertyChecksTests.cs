using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;
using NUnit.Framework;
using Worldbuilder;


namespace Tests
{
    [TestFixture]
    public class PropertyChecksTests
    {
       

        



    [Test]
        public void HasOwnIdTest()
        {
            Assert.AreEqual( (GetType(namedOwnId)).HasOwnId(), true );


        }
    }
}
