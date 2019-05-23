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
using UnitTests;

namespace UnitTests.TestClasses
{
    public class UserListControllerTests
    {
        private string ConnectionString = TestsDataProvider.GetConnectionString();

        [Fact]
        public void UserListController_GetUsers_ShouldGetAllUsersFromTheDatabase() {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new UserListController(context);
            var scans = context.Users.ToListAsync().Result;

            //Act
            var result = controller.GetUsers().Result.Value;

            //Assert
            Assert.Equal(scans, result);
        }        

        [Theory]
        [InlineData("a18be9c0-aa65-4af8-bd17-00bd93440001")]
        [InlineData("a18be9c0-aa65-4af8-bd17-00bd93440002")]     
        [InlineData("a18be9c0-aa65-4af8-bd17-00bd93440003")]
        public void UserListController_GetUsers_ShouldBeNotFoundOrOk(string id) {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new UserListController(context);

            //Act
            var result = controller.GetUser(id).Status;

            //Assert
            result.Should().Match<TaskStatus>(p => p == TaskStatus.Faulted || p == TaskStatus.Created || p == TaskStatus.WaitingForActivation);
        }

        [Theory]
        [InlineData("C00001")]
        [InlineData("C00002")]     
        [InlineData(":);):D")]
        public void UserListController_GetUserId_ShouldReturnStudentIdOrNull(string studentCode) {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new UserListController(context);

            //Act
            var result = controller.GetUserId(studentCode);

            //Assert
            if (result != null)
                result.Value.Should().Match(p => p is string || p == null);
            else
                result.Should().BeNull();
        }
    }
}