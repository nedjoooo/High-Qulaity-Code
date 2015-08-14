using System;
using CustomLinkedList;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CustomLinkedListTests
{
    [TestClass]
    public class CustomLinkedListTests
    {
        private DynamicList<int> dynamicList;

        [TestInitialize]
        public void InitDynamicList()
        {
            this.dynamicList = new DynamicList<int>();
        }

        [TestMethod]
        public void TestCountAfterAdding()
        {
            InitDynamicList();
            dynamicList.Add(5);
            dynamicList.Add(6);
            dynamicList.Add(7);
            dynamicList.Add(8);

            Assert.AreEqual(dynamicList.Count, 4);
        }

        [TestMethod]      
        public void TestCountAfterRemovingByItem()
        {
            InitDynamicList();          
            dynamicList.Add(8);
            dynamicList.Add(5);
            dynamicList.Add(3);
            dynamicList.Remove(5);

            Assert.AreEqual(dynamicList.Count, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemovingIndexLessByRangeOfIndices()
        {
            InitDynamicList();
            dynamicList.Add(8);
            dynamicList.Add(5);
            dynamicList.Add(3);
            dynamicList.RemoveAt(-3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemovingIndexLargerByRangeOfIndices()
        {
            InitDynamicList();
            dynamicList.Add(8);
            dynamicList.Add(5);
            dynamicList.Add(3);
            dynamicList.RemoveAt(8);
        }

        [TestMethod]
        public void TestrRemovingNotContainsItem()
        {
            InitDynamicList();
            dynamicList.Add(8);
            dynamicList.Add(5);
            int index = dynamicList.Remove(4);

            Assert.AreEqual(-1, index);
        }

        [TestMethod]
        public void TestCountAfterRemovingByIndex()
        {
            InitDynamicList();
            dynamicList.Add(8);
            dynamicList.Add(5);
            dynamicList.Add(3);
            dynamicList.RemoveAt(1);

            Assert.AreEqual(dynamicList.Count, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRemoveAfterRemovingFromEmptyDynamicList()
        {
            InitDynamicList();
            dynamicList.Remove(5);
        }

        [TestMethod]
        public void TestElementsAfterAdding()
        {
            InitDynamicList();
            
            for (int i = 0; i < 5; i++)
            {
                dynamicList.Add(i);
            }

            string strDynamicList = "";
            for (int i = 0; i < 5; i++)
            {
                if(dynamicList.Contains(i))
                {
                    strDynamicList += i.ToString();
                    dynamicList.Remove(i);
                }
                
            }

            Assert.AreEqual("01234", strDynamicList);
        }

        [TestMethod]
        public void TestContainsElementInDyanmicListAfterAdding()
        {
            InitDynamicList();
            dynamicList.Add(10);

            bool isContains = false;
            if(dynamicList.Contains(10))
            {
                isContains = true;
            }

            Assert.IsTrue(isContains);
        }

        [TestMethod]
        public void TestNotContainsElementInDyanmicListAfterAdding()
        {
            InitDynamicList();
            dynamicList.Add(10);

            bool isContains = true;
            if (!dynamicList.Contains(8))
            {
                isContains = false;
            }

            Assert.IsFalse(isContains);
        }

        [TestMethod]
        public void TestInsertItemToDynamicList()
        {
            InitDynamicList();
            dynamicList.Add(1);
            dynamicList.Add(3);
            dynamicList.Add(3);

            dynamicList[1] = 2;

            Assert.AreEqual(2, dynamicList[1]);
            
        }

        [TestMethod]
        public void TestInsertItemToDynamicListIntoSpecifiedPosition()
        {
            InitDynamicList();
            dynamicList.Add(1);

            dynamicList[0] = 3;

            Assert.AreEqual(3, dynamicList[0]);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsertItemToDynamicListIntoNotValidPosition()
        {
            InitDynamicList();
            dynamicList.Add(1);

            dynamicList[4] = 3;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetNotValidIndex()
        {
            InitDynamicList();
            int index = dynamicList[3];
        }
    }
}
