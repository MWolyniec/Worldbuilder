using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var noIdObj = new { justName = "name" };

            var onlyIdObj = new { Id = 3 };

            var twoFKIds = new { FirstId = 2, SecondId = 3 };

            var oneOwnIdOneFK = new { Id = 2, OtherId = 1 };

            var namedOwnId = new { namedOwnIdId = 1 };
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual((GetType(namedOwnId)).HasOwnId(), true);
        }
    }
}