using ConsoleApp.Models;
using ConsoleApp.Managers;

namespace ConsoleApp.Tests
{
    public class EventManagerTests
    {
        [Fact]
        public void EventManager_GetEvents_ReturnsList()
        {
            // Arrange

            var options = new ListOptions();

            // Act

            var result = EventManager.GetEvents(options);

            // Assert

            Assert.IsType<List<Event>>(result);
            Assert.NotNull(result);

        }


    }
}
