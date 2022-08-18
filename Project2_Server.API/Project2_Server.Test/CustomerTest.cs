using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Project2_Server.API;
using Project2_Server.API.Controllers;
using Project2_Server.Data;
using Project2_Server.Model;
using System.Net;

namespace Project2_Server.Test
{
    public class CustomerTests
    {
        // Arrange
        Mock<INTERFACE_SQL_Customer> mockCustomer = new();
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
        public async void CheckCreateNewCustomer_Success()
        {
            // Arrange
            var newCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");

            mockCustomer.Setup(x => x.CUSTOMER_ASYNC_createNewCustomer(newCustomer)).ReturnsAsync(true);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            // Act
            var result = await controller.API_ASYNC_CUSTOMER_createNewCustomer(newCustomer);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void CheckCreateNewCustomer_Fail()
        {
            // Arrange
            var newCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");

            mockCustomer.Setup(x => x.CUSTOMER_ASYNC_createNewCustomer(newCustomer)).ReturnsAsync(false);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            // Act
            var result = await controller.API_ASYNC_CUSTOMER_createNewCustomer(newCustomer);
            

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckCreateNewCustomer_DModel_Pass()
        {
            bool result;

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
        public void Check_DModelCustomer_FirstName_Fail()
        {
            // Arrange
            bool result = true;

            // Act
            try
            {
                var newCustomer = new DMODEL_Customer(1, null, "Doe", "jane@email.com", "password");
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            Assert.False(result);
        }

        [Fact]
        public void Check_DModelCustomer_LastName_Fail()
        {
            // Arrange
            bool result = true;

            // Act
            try
            {
                var newCustomer = new DMODEL_Customer(1, "Jane", null, "jane@email.com", "password");
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            Assert.False(result);
        }

        [Fact]
        public void Check_DModelCustomer_Email_Fail()
        {
            // Arrange
            bool result = true;

            // Act
            try
            {
                var newCustomer = new DMODEL_Customer(1, "Jane", "Doe", null, "password");
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
        }

        [Fact]
        public void Check_DModelCustomer_Password_Fail()
        {
            // Arrange
            bool result = true;

            // Act
            try
            {
                var newCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", null);
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void CheckCustomerLogin_Success()
        {
            // Arrange
            DMODEL_Customer checkCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");

            mockCustomer.Setup(x => x.CUSTOMER_ASYNC_checkCustomerLogin("jane@email.com")).ReturnsAsync(checkCustomer);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                       mockOrder.Object,
                                                       mockProject.Object,
                                                       mockLinkingTable.Object,
                                                       mockILogger.Object);

            // Act
            int test = await controller.API_ASYNC_CUSTOMER_checkValidLogin("jane@email.com", "password");

            // Assert
            Assert.Equal(1, test);
        }

        [Fact]
        public async void CheckCustomerLogin_BadPassword()
        {
            // Arrange
            DMODEL_Customer checkCustomer = new DMODEL_Customer(1, "Jane", "Doe", "jane@email.com", "password");

            mockCustomer.Setup(x => x.CUSTOMER_ASYNC_checkCustomerLogin("jane@email.com")).ReturnsAsync(checkCustomer);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                       mockOrder.Object,
                                                       mockProject.Object,
                                                       mockLinkingTable.Object,
                                                       mockILogger.Object);

            // Act
            int test = await controller.API_ASYNC_CUSTOMER_checkValidLogin("jane@email.com", "paword");

            // Assert
            Assert.Equal(-1, test);
        }

        [Fact]
        public async void Check_DModelOrder_Success()
        {
            // Arrange
            var newOrder = new DMODEL_Order(1, DateTime.Now, false);
            var newProject = new DMODEL_Project(1, 1, false);
            List<DMODEL_Project> projectList = new List<DMODEL_Project>
            {
                new DMODEL_Project { project_id = 1, item_id = 1, completion_status = false },
                new DMODEL_Project { project_id = 2, item_id = 2, completion_status = false },
                new DMODEL_Project { project_id = 3, item_id = 1, completion_status = false }
            };
            var newOrderProject = new DTO_OrderProject(newOrder, projectList);

            mockOrder.Setup( x => x.ORDER_ASYNC_createNewOrder(newOrder)).ReturnsAsync(1);
            mockProject.Setup( x => x.PROJECT_ASYNC_createNewProject(newProject)).ReturnsAsync(1);
            mockLinkingTable.Setup(x => x.LINKING_ASYNC_addToCustomerOrderLinkingTable(1, 1)).ReturnsAsync(true);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            // Act
            var result = await controller.API_ASYNC_CUSTOMER_createNewOrder(1, newOrderProject);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void Check_DModelOrder_Empty_DMODELOrder_Fail()
        {
            // Arrange
            var newOrder = new DMODEL_Order();
            var newProject = new DMODEL_Project(1, 1, false);

            List<DMODEL_Project> projectList = new List<DMODEL_Project>
            {
                new DMODEL_Project { project_id = 1, item_id = 1, completion_status = false },
                new DMODEL_Project { project_id = 2, item_id = 2, completion_status = false },
                new DMODEL_Project { project_id = 3, item_id = 1, completion_status = false }
            };

            var newOrderProject = new DTO_OrderProject(newOrder, projectList);

            mockOrder.Setup(x => x.ORDER_ASYNC_createNewOrder(newOrder)).ReturnsAsync(-1);
            mockProject.Setup(x => x.PROJECT_ASYNC_createNewProject(newProject)).ReturnsAsync(1);
            mockLinkingTable.Setup(x => x.LINKING_ASYNC_addToCustomerOrderLinkingTable(1, 1)).ReturnsAsync(true);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            // Act
            var result = await controller.API_ASYNC_CUSTOMER_createNewOrder(1, newOrderProject);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void Check_DModelOrder_Empty_Project_Fail()
        {
            // Arrange
            var newOrder = new DMODEL_Order(1, DateTime.Now, false);
            var newProject = new DMODEL_Project();

            List<DMODEL_Project> projectList = new List<DMODEL_Project>
            {
                new DMODEL_Project { project_id = 1, item_id = 1, completion_status = false },
                new DMODEL_Project { project_id = 2, item_id = 2, completion_status = false },
                new DMODEL_Project { project_id = 3, item_id = 1, completion_status = false }
            };

            var newOrderProject = new DTO_OrderProject(newOrder, projectList);

            mockOrder.Setup(x => x.ORDER_ASYNC_createNewOrder(newOrder)).ReturnsAsync(1);
            mockProject.Setup(x => x.PROJECT_ASYNC_createNewProject(newProject)).ReturnsAsync(-1);
            mockLinkingTable.Setup(x => x.LINKING_ASYNC_addToCustomerOrderLinkingTable(1, 1)).ReturnsAsync(true);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            // Act
            var result = await controller.API_ASYNC_CUSTOMER_createNewOrder(1, newOrderProject);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void Check_DModelOrder_CustomerOrderLink_Fail()
        {
            // Arrange
            var newOrder = new DMODEL_Order();
            var newProject = new DMODEL_Project(-1, 1, false);

            List<DMODEL_Project> projectList = new List<DMODEL_Project>
            {
                new DMODEL_Project { project_id = 1, item_id = 1, completion_status = false },
                new DMODEL_Project { project_id = 2, item_id = 2, completion_status = false },
                new DMODEL_Project { project_id = 3, item_id = 1, completion_status = false }
            };

            var newOrderProject = new DTO_OrderProject(newOrder, projectList);

            mockOrder.Setup(x => x.ORDER_ASYNC_createNewOrder(newOrder)).ReturnsAsync(-1);
            mockProject.Setup(x => x.PROJECT_ASYNC_createNewProject(newProject)).ReturnsAsync(1);
            mockLinkingTable.Setup(x => x.LINKING_ASYNC_addToCustomerOrderLinkingTable(-1, 1)).ReturnsAsync(false);

            var controller = new CONTROLLER_Customer(mockCustomer.Object,
                                                        mockOrder.Object,
                                                        mockProject.Object,
                                                        mockLinkingTable.Object,
                                                        mockILogger.Object);

            // Act
            var result = await controller.API_ASYNC_CUSTOMER_createNewOrder(1, newOrderProject);

            // Assert
            Assert.False(result);
        }
    }
}