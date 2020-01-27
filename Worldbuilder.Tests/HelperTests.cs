using NUnit.Framework;
using System.Collections.Generic;

namespace Worldbuilder.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EnumerableCanBecomeTwoIdenticalArrays()
        {
            var list = new List<string>() { "12", "32", "43", "", "---" };

            string[] array1;

            array1 = list.ToTwoArrays(out string[] array2);


            Assert.AreEqual(array1, array2);
            Assert.AreNotSame(array1, array2);
        }

        /*   [Test]
           public Task CanUpdateDbSetUsingSelectList()
           {
               var joinTableMock = new Mock<DbSet<Brick>>();
               var objectToUpdateMock = new Mock<Brick>();

               joinTableMock.Setup(table => table.)

               joinTableMock.Object.UpdateFromSelectList()

           }*/
    }
}