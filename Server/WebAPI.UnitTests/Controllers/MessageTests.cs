using System.Net.Http;
using System.Web.Http;
using API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAPI.UnitTests.Controllers
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void GetReturnsMessage()
        {
            // Arrange
            var controller = new MessageController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var response = controller.Get(1);
            
            // Assert
            string message;
            Assert.IsTrue(response.TryGetContentValue(out message));
            Assert.AreEqual("Default Value", message);
        }

        [TestMethod]
        public void PostCreatesMessage()
        {
            // Arrange
            var controller = new MessageController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var response = controller.Post("New Message");

            // Assert
            string message;
            Assert.IsTrue(response.TryGetContentValue(out message));
            Assert.AreEqual("New Message", message);
        }
    }
}
