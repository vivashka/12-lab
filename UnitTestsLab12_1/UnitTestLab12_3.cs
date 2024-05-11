using BaseClassEmoji;
using Lab12_3;

namespace UnitTestsLab12
{
    [TestClass]
    public class UnitTestLab12_3
    {
        [TestMethod]
        public void TestConstructorLength()
        {
            BinaryTree<Emoji> myTree = new(10);
            
            Assert.IsTrue(myTree.Count == 10);
        }
        
        [TestMethod]
        public void TestLeafsAmount()
        {
            BinaryTree<Emoji> myTree = new(10);

            int count = myTree.LeafsAmount();
            
            Assert.IsTrue(count > 3);
        }
        
        [TestMethod]
        public void TestDeleteTree()
        {
            BinaryTree<Emoji> myTree = new(10);

            myTree.DeleteTree();          
            Assert.IsTrue(myTree.Count == 0 && myTree.ChangeRoot() == false);
        }
        
        [TestMethod]
        public void TestTransform()
        {
            BinaryTree<Emoji> myTree = new(10);
            BinaryTree<Emoji> searchTree = myTree.TransformToFindTree();

            bool isClone = myTree.FindItem(myTree.root, searchTree.root.Data) != null;

            searchTree.root = new Point<Emoji>();
            
            bool isItemClone = myTree.FindItem(myTree.root, searchTree.root.Data) == null;
            
            Assert.IsTrue(isClone && isItemClone);
        }
    }
}