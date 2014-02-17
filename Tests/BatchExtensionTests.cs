using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnumerableExtensions;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class BatchExtensionTests
    {
        private Stub _stub;
        private IEnumerable<int> _source;

        [SetUp]
        public void Setup()
        {
            _stub = new Stub();
            _source = _stub.Take(20);
        }

        [Test]
        public void EnumeratesOnlyOnce()
        {
            var groups = _source.Batch(7);

            _stub.CallsToGetEnumerator.ShouldBe(0);

            groups.Count();

            _stub.CallsToGetEnumerator.ShouldBe(1);
        }

        [Test]
        public void EnumerateItemsForEachBatch()
        {
            var groups = _source.Batch(3);

            var expectedYields = 0;
            foreach (var group in groups)
            {
                var items = group.Count();
                expectedYields = expectedYields + items;
                _stub.IndexOfYield.ShouldBe(expectedYields);
            }
        }

        [Test]
        public void PrintGroups()
        {
            var groups = _source.Batch(3);
            foreach (var group in groups)
            {
                Console.WriteLine(String.Join(", ", group));
            }
        }

        [Test]
        public void ReverseWillEnumerateToEndBeforeBatching()
        {
            var reverse = _source.Batch(3).Reverse();
            foreach (var group in reverse)
            {
                _stub.IndexOfYield.ShouldBe(20);
                Console.WriteLine(String.Join(", ", group));
            }
        }
    }
}