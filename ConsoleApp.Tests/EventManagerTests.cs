using ConsoleApp.Models;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    public class EventManagerTests
    {
        [Fact]
        public void EventManager_GetEvents_ReturnsList()
        {
            // Arrange

            var options = new Options
            {
                 IsToday = true,
                 IsExcluded = true,
            };

            // Act

            var result = EventManager.GetEvents(options);

            // Assert

            Assert.IsType<List<Event>>(result);
            Assert.NotNull(result);

        }

        [Theory]

    }
}
