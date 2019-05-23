using System;
using System.Net;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using NFCSystem;
using NFCSystem.Controllers;
using NFCSystem.Data;
using NFCSystem.Models;
using NFCSystem.Models.Timetables;

namespace UnitTests.TestClasses
{
    public class ControllerTests
    {  
        private string ConnectionString = TestsDataProvider.GetConnectionString();

        [Fact]
        public void LecturesController_GetClassrooms_ShouldGetAllClassroomsFromTheDatabase() {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new LecturesController(context);
            var classrooms = context.Classrooms.ToListAsync().Result;

            //Act
            var result = controller.GetClassrooms().Result.Value;

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(context.Classrooms, result);
        }

        // These may fail if users are deleted x(
        [Theory]
        [InlineData("a18be9c0-aa65-4af8-bd17-00bd93440001")]
        [InlineData("a18be9c0-aa65-4af8-bd17-00bd93440002")]     
        [InlineData("a18be9c0-aa65-4af8-bd17-00bd93440003")]
        public void LecturesController_Get_ShouldGetAsMuchLecturesAsThereAreInTheDatabaseForIndividualIDs(string id) {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new LecturesController(context);
            var lecturesCount = context.Timetables.Where(x => x.StudentId == id).ToList().Count;

            //Act
            var result = controller.Get(id).Value.Count;

            //Assert
            Assert.Equal(lecturesCount, result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]     
        [InlineData("3")]
        [InlineData(null)]
        public void LecturesController_Get_ShouldReturnNotFoundIfIDIsNotValid(string id) {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new LecturesController(context);

            //Act
            var result = controller.Get(id).Value;

            //Assert
            result.Should().BeNullOrEmpty();
        }
    }
}