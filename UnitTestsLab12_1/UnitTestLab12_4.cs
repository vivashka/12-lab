

using BaseClassEmoji;
using Lab12_4;

namespace UnitTestsLab12
{
    [TestClass]
    public class UnitTestLab12_4
    {
        [TestMethod]
        public void TestConstructorLength()
        {
            MyCollection<Emoji> myTree = new(10);
            
            Assert.IsTrue(myTree.Count == 10);
        }
        
        [TestMethod]
        public void TestAdd()
        {
            MyCollection<Emoji> myTree = new(10);

            Emoji emoji = new Emoji();
            emoji.RandomInit();
            myTree.Add(emoji);
            
            Assert.IsTrue(myTree.Contains(emoji));
        }
        
        [TestMethod]
        public void TestRemove()
        {
            MyCollection<Emoji> myTree = new(10);

            Emoji emoji = (Emoji)myTree.root.Data.Clone();
            emoji.id.Number = emoji.id.Number;
            
            myTree.Remove(myTree.root.Data);
            
            Assert.IsTrue(!myTree.Contains(emoji));
        }
        
        [TestMethod]
        public void TestCloneCopy()
        {
            MyCollection<Emoji> myTree = new(10);
            MyCollection<Emoji> clone = new(myTree);
            Emoji[] copy = new Emoji[10];

            clone.root = new Point<Emoji>();
            myTree.CopyTo(copy, 5);
            bool isContains = true;
            int i = 5;
            while (isContains && i < copy.Length)
            {
                isContains = myTree.Contains(copy[i]);
                i++;
            }
            
            Assert.IsTrue(!myTree.Contains(clone.root.Data) && isContains);
        }
        
        [TestMethod]
        public void TestClear()
        {
            MyCollection<Emoji> myTree = new(10);
            
            myTree.Clear();

            Assert.IsTrue(myTree.root == null && myTree.Count == 0);
        }
    }
}