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
using JwtAuthentication.Controllers;

namespace UnitTests.TestClasses
{
    public class AuthControllerTests
    {        
        private string ConnectionString = TestsDataProvider.GetConnectionString();
    }
}