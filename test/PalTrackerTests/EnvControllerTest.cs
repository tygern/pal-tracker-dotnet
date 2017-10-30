﻿using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PalTracker;
using PalTracker.Controllers;
using Xunit;

namespace PalTrackerTests
{
    public class EnvControllerTest
    {
        [Fact]
        public void Get()
        {
            var cfInfo = new CfInfo(
                "8080",
                "512M",
                "1",
                "127.0.0.1"
            );

            var response = new EnvController(cfInfo).Get();

            Assert.Equal("8080", response.Port);
            Assert.Equal("512M", response.MemoryLimit);
            Assert.Equal("1", response.CfInstanceIndex);
            Assert.Equal("127.0.0.1", response.CfInstanceAddr);
        }
    }
}