using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LinqExtensions;
using NUnit.Framework;

namespace LinqExtensionsTests {
    [TestFixture]
    public class LinqExtensionsTests {
        [Test]
        public void Given_Collcetion_Of_Numbers_When_GroupAdjecantBy_Then_Returns_Collection_Of_Adjecant_Groups() {
            var numbers = new int[] { 1, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0 };

            var groups = numbers.GroupAdjecantBy(i => i);
            var expectedGropus = new List<IGrouping<int, int>>() {
                new GroupOfAdjacent<int,int> (new List<int> {1},     1),
                new GroupOfAdjacent<int,int> (new List<int> {0,0},   0),
                new GroupOfAdjacent<int,int> (new List<int> {2},     2),
                new GroupOfAdjacent<int,int> (new List<int> {0,0,0}, 0),
                new GroupOfAdjacent<int,int> (new List<int> {2},     2),
                new GroupOfAdjacent<int,int> (new List<int> {0,0,0}, 0)
            };

            CollectionAssert.AreEqual(expectedGropus, groups);
        }

        [Test]
        public void Given_Empty_Collcetion_Of_Numbers_When_GroupAdjecantBy_Then_Returns_Empty_Collection_Of_Adjecant_Groups() {
            var numbers = new int[0];

            var groups = numbers.GroupAdjecantBy(i => i);
            CollectionAssert.IsEmpty(groups);
        }
    }
}
