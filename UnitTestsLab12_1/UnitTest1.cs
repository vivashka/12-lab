using BaseClassEmoji;
using lab;

namespace UnitTestsLab12
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreation()
        {
            MyList<Emoji> myList = new MyList<Emoji>(10);

            Assert.IsTrue(myList.Count == 10);
        }
    }
}