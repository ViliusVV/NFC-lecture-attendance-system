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

namespace UnitTests.TestClasses
{
    public class NFCScanControllerTests
    {
        private string ConnectionString = TestsDataProvider.GetConnectionString();

        [Fact]
        public void NFCScanController_GetScans_ShouldGetAllScansFromTheDatabase() {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new NFCScanController(context);
            var scans = context.NFCScans.ToListAsync().Result;

            //Act
            var result = controller.GetScans().Result.Value;

            //Assert
            Assert.Equal(scans, result);
        }

        [Theory]
        [InlineData(169029520)]
        [InlineData(429608434)]     
        [InlineData(100000000)]
        public void NFCScanController_GetScan_ShouldBeNotFoundOrOk(long id) {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new NFCScanController(context);

            //Act
            var result = controller.GetScan(id).Status;

            //Assert
            result.Should().Match<TaskStatus>(p => p == TaskStatus.Faulted || p == TaskStatus.Created || p == TaskStatus.WaitingForActivation);
        }

        [Fact]
        public void NFCScanController_GetDevices_ShouldGetAllDevicesFromTheDatabase() {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new NFCScanController(context);
            var scans = context.Devices.ToListAsync().Result;

            //Act
            var result = controller.GetDevices().Result.Value;

            //Assert
            Assert.Equal(scans, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]     
        [InlineData(666)]
        public void NFCScanController_GetDeviceId_ShouldBeNotFoundOrOk(int id) {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new NFCScanController(context);

            //Act
            var result = controller.GetDeviceId(id).Status;

            //Assert
            result.Should().Match<TaskStatus>(p => p == TaskStatus.Faulted || p == TaskStatus.Created || p == TaskStatus.WaitingForActivation);
        }
    }
}