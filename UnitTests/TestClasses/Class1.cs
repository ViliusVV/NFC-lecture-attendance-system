using Xunit;
using NFCSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UnitTests
{
    public class Class1
    {
        // Fact test example
        // Facts are tests which are always true. They test invariant conditions
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(4, Add(2, 2)); // should be 5 instead of 4 if u want it to fail
        }

        int Add(int x, int y)
        {
            return x + y;
        }

        // Theory test example
        // Theories are tests which are only true for a particular set of data
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(1)]  // fails if 2,4,6,...
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        bool IsntOdd(int value)
        {
            return value % 2 != 1;
        }

        // ValuesControllerTest is just an example
        [Fact]
        public void ValuesControllerTest() {
            // Arrange
            var controller = new ValuesController();

            // Act
            var result = controller.Get();

            // Assert
            var iEnum = Assert.IsType<ActionResult<IEnumerable<string>>>(result);
            Assert.Equal(new string[] { "value1", "value2" }, iEnum.Value);
        }
    }
}
