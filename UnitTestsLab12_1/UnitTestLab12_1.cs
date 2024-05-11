using BaseClassEmoji;
using lab;

namespace UnitTestsLab12
{
    [TestClass]
    public class UnitTestLab12_1
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
        
        [TestMethod]
        public void TestAddOdd()
        {
            MyList<Emoji> myList = new MyList<Emoji>(5);
            
            myList.AddOddItem();
            
            Assert.IsTrue(myList.Count == 9);
        }
        
        [TestMethod]
        public void TestChangeItem()
        {
            MyList<Emoji> myList = new MyList<Emoji>();
            
            bool expected1 = myList.ChangeItem();

            Emoji e = new Emoji();
            e.RandomInit();
            myList.AddToEnd(e);
            
            bool expected2 = myList.ChangeItem();
            
            Assert.IsTrue(expected1 != expected2);
        }
        
        [TestMethod]
        public void TestRemoveItem()
        {
            Emoji[] arrExpected = new Emoji[10];
            
            for (int i = 0; i < arrExpected.Length; i++)
            {
                arrExpected[i] = new Emoji();
                arrExpected[i].RandomInit();
            }
            
            MyList<Emoji> myList = new MyList<Emoji>(arrExpected);

            myList.RemoveItem(arrExpected[9].Tag);
            myList.RemoveItem(arrExpected[4].Tag);
            myList.RemoveItem(arrExpected[0].Tag);
            
            Assert.IsTrue(myList.Count == 7);
        }
        
        [TestMethod]
        public void TestClear()
        {
            MyList<Emoji> myList = new MyList<Emoji>(10);

            myList.Clear();
            
            Assert.IsTrue(myList.Count == 0 && myList.AddOddItem() == false);
        }
        
        [TestMethod]
        public void TestClone()
        {
            MyList<Emoji> myList = new MyList<Emoji>(10);
            MyList<Emoji> clone;

            Emoji expected1 = new Emoji();
            expected1.RandomInit();
            myList.AddToEnd(expected1);

            clone = myList.Clone();
            Emoji expected2 = new Emoji();
            expected2.RandomInit();
            clone.AddToEnd(expected2);

            bool isFind = myList.FindItem(expected1.Tag).Data.Equals(clone.FindItem(expected1.Tag).Data) ;
            bool isMiss = myList.FindItem(expected2.Tag) == null;
            
            Assert.IsTrue(isFind == isMiss);
        }
    }
}