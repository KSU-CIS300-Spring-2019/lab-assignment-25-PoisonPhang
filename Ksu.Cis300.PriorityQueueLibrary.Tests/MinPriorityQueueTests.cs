/* MinPriorityQueueTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Ksu.Cis300.PriorityQueueLibrary.Tests
{
    /// <summary>
    /// Tests for MinPriorityQueue and LeftistTree.
    /// </summary>
    [TestFixture]
    public class MinPriorityQueueTests
    {
        /// <summary>
        /// Tests that the null path length of an empty leftist tree is 0.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAEmptyNpl()
        {
            Assert.That(LeftistTree<string>.NullPathLength(null), Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that the null path length of a 1-node leftist tree is 1.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBSingleNodeNpl()
        {
            Assert.That(LeftistTree<int>.NullPathLength(new LeftistTree<int>(7, null, null)), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that a leftist tree constructed with a 1-node tree as the first child and an empty
        /// tree as the second child has a null path length of 1.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCTwoNodeFirstNpl()
        {
            LeftistTree<int> child = new LeftistTree<int>(2, null, null);
            LeftistTree<int> t = new LeftistTree<int>(5, child, null);
            Assert.That(LeftistTree<int>.NullPathLength(t), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that a leftist tree constructed with an empty tree as the first child and a 1-node tree
        /// as the second child has a null path length of 1.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCTwoNodeSecondNpl()
        {
            LeftistTree<int> child = new LeftistTree<int>(2, null, null);
            LeftistTree<int> t = new LeftistTree<int>(5, null, child);
            Assert.That(LeftistTree<int>.NullPathLength(t), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that a leftist tree constructed with a 1-node tree as the first child and an
        /// empty tree as the second child has the correct shape and contents.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDTwoNodeFirstShape()
        {
            LeftistTree<int> child = new LeftistTree<int>(2, null, null);
            LeftistTree<int> t = new LeftistTree<int>(5, child, null);
            Assert.Multiple(() =>
            {
                Assert.That(t.Data, Is.EqualTo(5));
                Assert.That(t.LeftChild, Is.EqualTo(child));
                Assert.That(t.RightChild, Is.Null);
            });
        }

        /// <summary>
        /// Tests that a leftist tree constructed with an empty tree as the first child
        /// and a 1-node tree as the second child has the correct shape and contents.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDTwoNodeSecondShape()
        {
            LeftistTree<int> child = new LeftistTree<int>(2, null, null);
            LeftistTree<int> t = new LeftistTree<int>(5, null, child);
            Assert.Multiple(() =>
            {
                Assert.That(t.Data, Is.EqualTo(5));
                Assert.That(t.LeftChild, Is.EqualTo(child));
                Assert.That(t.RightChild, Is.Null);
            });
        }

        /// <summary>
        /// Tests that a leftist tree with two single-node children has a null path length
        /// of 2.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEThreeNodeNpl()
        {
            LeftistTree<int> child1 = new LeftistTree<int>(8, null, null);
            LeftistTree<int> child2 = new LeftistTree<int>(3, null, null);
            LeftistTree<int> t = new LeftistTree<int>(4, child1, child2);
            Assert.That(LeftistTree<int>.NullPathLength(t), Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that a leftist tree with two 1-node children has the correct shape
        /// and contents.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEThreeNodeShape()
        {
            int[] children = { 3, 9 };
            LeftistTree<int> t = new LeftistTree<int>(5, new LeftistTree<int>(children[0], null, null),
                new LeftistTree<int>(children[1], null, null));
            int[] test = { t.LeftChild.Data, t.RightChild.Data };
            Assert.Multiple(() =>
            {
                Assert.That(t.Data, Is.EqualTo(5));
                Assert.That(test, Is.EquivalentTo(children));
            });
        }

        /// <summary>
        /// Tests that merging two empty leftist heaps gives an empty leftist heap.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFMergeTwoEmpty()
        {
            Assert.That(MinPriorityQueue<int, string>.Merge(null, null), Is.Null);
        }

        /// <summary>
        /// Tests that merging an empty leftist heap and a 1-node leftist heap gives the 1-node leftist heap.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFMergeEmptyAndOneNode()
        {
            LeftistTree<KeyValuePair<int, string>> t = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(2, "two"), null, null);
            Assert.That(MinPriorityQueue<int, string>.Merge(null, t), Is.EqualTo(t));
        }

        /// <summary>
        /// Tests that merging a 1-node leftist heap and an empty leftist heap gives the 1-node leftist heap.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFMergeOneNodeAndEmpty()
        {
            LeftistTree<KeyValuePair<int, string>> t = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(2, "two"), null, null);
            Assert.That(MinPriorityQueue<int, string>.Merge(t, null), Is.EqualTo(t));
        }

        /// <summary>
        /// Tests the merging of two single nodes, where the first has the smaller key.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestGMergeSingleNodesSmallerLarger()
        {
            LeftistTree<KeyValuePair<int, string>> t1 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(2, "two"), null, null);
            LeftistTree<KeyValuePair<int, string>> t2 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(5, "five"), null, null);
            LeftistTree<KeyValuePair<int, string>> result = MinPriorityQueue<int, string>.Merge(t1, t2);
            Assert.Multiple(() =>
            {
                Assert.That(result.Data, Is.EqualTo(t1.Data));
                Assert.That(result.LeftChild, Is.EqualTo(t2));
                Assert.That(result.RightChild, Is.Null);
            });
        }

        /// <summary>
        /// Tests the merging of two single nodes, where the first has the larger key.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestGMergeSingleNodesLargerSmaller()
        {
            LeftistTree<KeyValuePair<int, string>> t1 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(2, "two"), null, null);
            LeftistTree<KeyValuePair<int, string>> t2 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(5, "five"), null, null);
            LeftistTree<KeyValuePair<int, string>> result = MinPriorityQueue<int, string>.Merge(t2, t1);
            Assert.Multiple(() =>
            {
                Assert.That(result.Data, Is.EqualTo(t1.Data));
                Assert.That(result.LeftChild, Is.EqualTo(t2));
                Assert.That(result.RightChild, Is.Null);
            });
        }

        /// <summary>
        /// Adds all the key-value pairs in the given leftist heap to the end of the given list.
        /// </summary>
        /// <param name="t">The leftist heap.</param>
        /// <param name="list">The list to which the key-value pairs in t will be added.</param>
        private void AddAll(LeftistTree<KeyValuePair<int, string>> t, List<KeyValuePair<int, string>> list)
        {
            if (t != null)
            {
                list.Add(t.Data);
                AddAll(t.LeftChild, list);
                AddAll(t.RightChild, list);
            }
        }

        /// <summary>
        /// Tests merging a 3-node leftist heap with a one-node leftist heap.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHMergeGeneral()
        {
            LeftistTree<KeyValuePair<int, string>> t1Child1 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(5, "five"), null, null);
            LeftistTree<KeyValuePair<int, string>> t1Child2 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(6, "six"), null, null);
            LeftistTree<KeyValuePair<int, string>> t1 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(2, "two"), t1Child1, t1Child2);
            LeftistTree<KeyValuePair<int, string>> t2 = new LeftistTree<KeyValuePair<int, string>>(new KeyValuePair<int, string>(4, "four"), null, null);
            LeftistTree<KeyValuePair<int, string>> result = MinPriorityQueue<int, string>.Merge(t1, t2);
            List<KeyValuePair<int, string>> contents = new List<KeyValuePair<int, string>>();
            AddAll(result, contents);
            Assert.That(contents, Is.EquivalentTo(new KeyValuePair<int, string>[] { t1.Data, t1.LeftChild.Data, t1.RightChild.Data, t2.Data }));
        }

        /// <summary>
        /// Tests that removing from an empty min-priority queue throws the correct exception.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestIRemoveFromEmptyQueue()
        {
            Exception e = null;
            try
            {
                new MinPriorityQueue<long, IList<int>>().RemoveMinimumPriority();
            }
            catch (Exception ex)
            {
                e = ex;
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that finding the minimum priority on an empty queue throws the correct exception.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestIMinimumPriorityFromEmptyQueue()
        {
            Exception e = null;
            try
            {
                int i = new MinPriorityQueue<int, string>().MinimumPriority;
            }
            catch (Exception ex)
            {
                e = ex;
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that the MinimumPriority property returns the minimum priority.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestJMinimumPriority()
        {
            MinPriorityQueue<int, string> pq = new MinPriorityQueue<int, string>();
            pq.Add(5, "five");
            pq.Add(2, "two");
            pq.Add(6, "six");
            Assert.That(pq.MinimumPriority, Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that the first RemoveMinimumPriority returns the correct value.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestJRemoveMinimumPriority()
        {
            MinPriorityQueue<int, string> pq = new MinPriorityQueue<int, string>();
            pq.Add(5, "five");
            pq.Add(2, "two");
            pq.Add(6, "six");
            string s = pq.RemoveMinimumPriority();
            Assert.That(s, Is.EqualTo("two"));
        }

        /// <summary>
        /// Tests the Count property after a RemoveMinimumPriority.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestKCountAfterRemove()
        {
            MinPriorityQueue<int, string> pq = new MinPriorityQueue<int, string>();
            pq.Add(5, "five");
            pq.Add(2, "two");
            pq.Add(6, "six");
            pq.RemoveMinimumPriority();
            Assert.That(pq.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// Tests the Count property after a MinimumPriority.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestKCountAfterMinimumPriority()
        {
            MinPriorityQueue<int, string> pq = new MinPriorityQueue<int, string>();
            pq.Add(5, "five");
            pq.Add(2, "two");
            pq.Add(6, "six");
            int i = pq.MinimumPriority;
            Assert.That(pq.Count, Is.EqualTo(3));
        }

        /// <summary>
        /// Tests removing the second priority from the queue.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestLRemoveTwo()
        {
            MinPriorityQueue<int, string> pq = new MinPriorityQueue<int, string>();
            pq.Add(5, "five");
            pq.Add(2, "two");
            pq.Add(6, "six");
            pq.RemoveMinimumPriority();
            string s = pq.RemoveMinimumPriority();
            Assert.That(s, Is.EqualTo("five"));
        }

        /// <summary>
        /// Tests sorting using a min-priority queue.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestMSort()
        {
            int[] priorities = { 4, 2, 9, 7, 0, 8, 1, 3, 5, 6 };
            string[] values = { "4", "2", "9", "7", "0", "8", "1", "3", "5", "6" };
            MinPriorityQueue<int, string> pq = new MinPriorityQueue<int, string>();
            for (int i = 0; i < priorities.Length; i++)
            {
                pq.Add(priorities[i], values[i]);
            }
            List<string> result = new List<string>();
            while (pq.Count > 0)
            {
                result.Add(pq.RemoveMinimumPriority());
            }
            Assert.That(result, Is.Ordered.And.EquivalentTo(values));
        }
    }
}