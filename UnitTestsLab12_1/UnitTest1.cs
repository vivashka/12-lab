using BaseClassEmoji;
using lab;

namespace UnitTestsLab12
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructorLength()
        {
            MyList<Emoji> myList = new MyList<Emoji>(10);
            
            Assert.IsTrue(myList.Count == 10);
        }

        [TestMethod]
        public void TestConstructorCopy()
        {
            MyList<Emoji> myList = new MyList<Emoji>();
            Emoji[] arrEmojis = new Emoji[10];

            for (int i = 0; i < arrEmojis.Length; i++)
            {
                arrEmojis[i] = new Emoji();
                arrEmojis[i].RandomInit();
                myList.AddToEnd(arrEmojis[i]);
            }

            Assert.IsTrue(myList.Count == 10);
        }

        [TestMethod]
        public void TestFindItem()
        {
            MyList<Emoji> myList = new MyList<Emoji>();

            Emoji expected = new Emoji();
            myList.AddToEnd(expected);
            
            Assert.AreEqual(myList.FindItem("smile").Data.Tag, expected.Tag);
        }
    }
}