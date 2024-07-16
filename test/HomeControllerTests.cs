using Xunit;
using app.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace app.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(mockLogger.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }
    }
}