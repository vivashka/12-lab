using AnimalEmoji;
using BaseClassEmoji;
using Lab12_2;

namespace UnitTestsLab12
{
    [TestClass]
    public class UnitTestLab12_2
    {
        [TestMethod]
        public void TestConstructorLength()
        {
            MyHashTable<Emoji> myList = new MyHashTable<Emoji>(10);
            
            Assert.IsTrue(myList.Capacity == 10 && myList.Count == 0);
        }
        
        [TestMethod]
        public void TestAddItem()
        {
            MyHashTable<Emoji> myList = new MyHashTable<Emoji>(10);

            Emoji emoji = new Emoji();
            for (int i = 0; i < 20; i++)
            {
                emoji = new Emoji();
                emoji.RandomInit();
                myList.AddItem(emoji.Tag, emoji);
            }
            
            Assert.IsTrue(myList.Contains(emoji.Tag));
        }
        
        [TestMethod]
        public void TestRemoveItem()
        {
            MyHashTable<Emoji> myList = new MyHashTable<Emoji>(10);
            
            Emoji emoji = new Emoji(){Tag = "random"};
            myList.AddItem(emoji.Tag, emoji);
            bool isExist = myList.Contains("random");

            bool isDelete = myList.RemoveData("random");
            
            bool isAlreadyDelete = !myList.RemoveData("random");
            
            Assert.IsTrue(isExist == isDelete == isAlreadyDelete);
        }
        
        [TestMethod]
        public void TestFindItem()
        {
            MyHashTable<Emoji> myList = new MyHashTable<Emoji>(10);
            
            
            myList.AddItem("terror", new Emoji());

            myList.RemoveData("terror");
            
            Assert.IsTrue(!myList.Contains("terror"));
        }
    }
}