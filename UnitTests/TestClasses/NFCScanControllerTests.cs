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

        [Fact]
        public async void NFCScanController_PostDevice_ShouldUpdateExistingDeviceInDatabase() {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new NFCScanController(context);

            var device = new Device()
            {
                DeviceId = 20,
                DeviceIdReal = 666,
                PendingDeviceId = 666,
                updatePending = false,
                ClassroomId = 9999
            };

            //Act
            await controller.PostDevice(device);
            var updatedDevice = context.Devices.Where(x => x.DeviceId == 20).First();

            //Assert
            Assert.True(updatedDevice.DeviceIdReal == device.DeviceIdReal);
        }

        // May fail if scan with ScanId=Int32.MaxValue is not deleted
        [Fact]
        public async void NFCScanController_PostScan_ShouldMarkTheLectureAsVisited() {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConnectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new NFCScanController(context);

            var scan = new NFCScan()
            {
                ScanId = context.NFCScans.Max(x => x.ScanId) + 1,
                UID = 190704260,
                TimeStamp = new DateTime(2019, 3, 14, 15, 35, 0),
                DeviceId = 19
            };

            //Act
            await controller.PostScan(scan);
            var markedAsVisited = context.Timetables.Where(x => x.TimetableId == 5632).First().isVisited;

            //Assert
            Assert.True(markedAsVisited);

            //Deletes created fake scan
            context.NFCScans.Remove(scan);
            //Makes that lecture not visited again
            context.Timetables.Where(x => x.TimetableId == 5632).First().isVisited = false;
            context.SaveChanges();
        }
    }
}