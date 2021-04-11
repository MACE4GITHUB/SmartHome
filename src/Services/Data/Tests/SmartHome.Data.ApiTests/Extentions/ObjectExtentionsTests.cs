using System;
using FluentAssertions;
using Xunit;

namespace SmartHome.Data.Api.Extentions.Tests
{
    public class ObjectExtentionsTests
    {
        [Fact]
        public void ObjectAddArgumentNullExceptionObjectsTest()
        {
            var objects = Array.Empty<object>();
            var items = Array.Empty<object>();

            Action action = () => objects.ObjectAdd(items);
            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'objects')");
        }

        [Fact]
        public void ObjectAddArgumentNullExceptionItemsTest()
        {
            var objects = new object[] { 0 };
            var items = Array.Empty<object>();

            Action action = () => objects.ObjectAdd(items);
            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'items')");
        }

        [Fact]
        public void ObjectAddNewItemsTest()
        {
            var objects = new object[] { 0 };
            var items = new object[] { 1 };

            objects.ObjectAdd(items).Length.Should().Be(2);
        }

        [Fact]
        public void IsNullOrEmptyInNullTest()
        {
            object[] objects = null;
            objects.IsNullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmptyInEmptyTest()
        {
            var objects = Array.Empty<object>();
            objects.IsNullOrEmpty().Should().BeTrue();
        }


    }
}