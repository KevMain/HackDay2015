using System.Net.Http;
using System.Web.Http;
using API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAPI.UnitTests.Controllers
{
    [TestClass]
    public class MessageTests
    {
        private MessageController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new MessageController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        //[TestMethod]
        //public void GetReturnsMessage()
        //{
        //    // Arrange
        //    const int defaultId = 1;
        //    const string expectedValue = "Default Value";

        //    // Act
        //    var response = _controller.Get(defaultId);
            
        //    // Assert
        //    string message;
        //    Assert.IsTrue(response.TryGetContentValue(out message));
        //    Assert.AreEqual(expectedValue, message);
        //}

        //[TestMethod]
        //public void PostCreatesMessage()
        //{
        //    // Arange
        //    const string newMessage = "New Message";

        //    // Act
        //    var response = _controller.Post(newMessage);

        //    // Assert
        //    string message;
        //    Assert.IsTrue(response.TryGetContentValue(out message));
        //    Assert.AreEqual(newMessage, message);
        //}
    }
}
