using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Project2_Server.API.Controllers;
using Project2_Server.Data;
using Project2_Server.Model;
using System.Net;
using Xunit;

namespace Project2_Server.Test
{
    public class CustomerTests
    {
        // Arrange
        Mock<INTERFACE_SQL_Customer> mockRepo = new();
        Mock<INTERFACE_SQL_Order> mockOrder = new();
        Mock<INTERFACE_SQL_Employee> mockEmployee = new();
        Mock<INTERFACE_SQL_Project> mockProject = new();
        Mock<INTERFACE_SQL_LinkingTable> mockLinkingTable = new();
        Mock<ILogger<CONTROLLER_Customer>> mockILogger = new();


        public class HttpMessageHandlerMock : HttpMessageHandler
        {
            private readonly HttpStatusCode _code;
            public HttpMessageHandlerMock(HttpStatusCode code)
            {
                _code = code;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = _code
                });
            }
        }

        [Fact]
        public async void checkCreateNewCustomer_Success()
        {
            // Arrange
            var newCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");

            //mockRepo.Setup(x => x.CUSTOMER_ASYNC_createNewCustomer(newCustomer)).ReturnsAsync(true);
            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.OK));

            var controller = new CONTROLLER_Customer(mockRepo.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            var result = await controller.API_ASYNC_CUSTOMER_createNewCustomer(newCustomer);
            // Act

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void checkCreateNewCustomer_Fail()
        {
            // Arrange
            var newCustomer = new DMODEL_Customer(1, null, "Doe", "jane@email.com", "password");

            //mockRepo.Setup(x => x.CUSTOMER_ASYNC_createNewCustomer(newCustomer)).ReturnsAsync(true);
            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.BadRequest));

            var controller = new CONTROLLER_Customer(mockRepo.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            var result = await controller.API_ASYNC_CUSTOMER_createNewCustomer(newCustomer);
            // Act

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void checkCreateNewCustomer_Pass()
        {
            bool result = false;

            try
            {
                var newCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void checkCreateNewCustomerDModel_Fail()
        {
            bool result = false;

            try
            {
                var newCustomer = new DMODEL_Customer(1, null, "Doe", "jane@email.com", "password");
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }

            // Assert
            Assert.True(result);
        }


        [Fact]
        public void checkCustomerLoginSQL()
        {
            // Arrange
            DMODEL_Customer checkCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");

            mockRepo.Setup(x => x.CUSTOMER_ASYNC_checkCustomerLogin("jane@email.com")).ReturnsAsync(checkCustomer);
        }

        //[Fact]
        //public void checkGetUserSQL()
        //{
        //    // Arrange
        //    DMODEL_Customer checkCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");


        //    mockRepo.
        //        (x => x.CUSTOMER_ASYNC_getCustomerData(1)).ThrowsAsync<ArgumentNullException>(checkCustomer);
        //}

        //[Fact]
        //public async void ShouldReturnPosts()
        //{
        //   // Arrange
        //   var handlerMock = new Mock<HttpMessageHandler>();
        //    var response = new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
        //    };


        //    handlerMock
        //       .Protected()
        //       .Setup<Task<HttpResponseMessage>>(
        //          "SendAsync",
        //          ItExpr.IsAny<HttpRequestMessage>(),
        //          ItExpr.IsAny<CancellationToken>())
        //       .ReturnsAsync(response);
        //    var httpClient = new HttpClient(handlerMock.Object);
        //    var posts = new Posts(httpClient);

        //    var retrievedPosts = await posts.GetPosts();

        //    Assert.NotNull(retrievedPosts);
        //    handlerMock.Protected().Verify(
        //       "SendAsync",
        //       Times.Exactly(1),
        //       ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
        //       ItExpr.IsAny<CancellationToken>());
        //}
    }
}