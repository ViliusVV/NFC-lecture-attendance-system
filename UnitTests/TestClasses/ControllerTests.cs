using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NFCSystem.Controllers;
using NFCSystem.Data;
using NFCSystem.Models;
using NFCSystem.Models.Timetables;

namespace UnitTests
{
    public class ControllerTests
    {
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