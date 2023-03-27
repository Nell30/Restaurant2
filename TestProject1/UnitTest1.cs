using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using WeeklyTask.Controllers;
using WeeklyTask.Areas.Identity.Data;
using WeeklyTask.Models.Helpers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeeklyTask.Tests.Controllers
{
    public class HomeControllerTests
    {

        /*public async Task Index_ReturnsView()
        {
            // Arrange

            // Create a mock of the UserManager class for the ApplicationUser class, which provides methods for user management.
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            // Setup the mock to return a null ApplicationUser when the FindByEmailAsync method is called with any email address.
            userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Create a mock of the RoleManager class for the IdentityRole class, which provides methods for role management.
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            // Create a mock of the IOptions interface for the StripeSettings class, which provides access to the Stripe API settings.
            var stripeSettingsMock = new Mock<IOptions<StripeSettings>>();

            // Create a new instance of the HomeController class, passing in the mocked dependencies.
            var controller = new HomeController(userManagerMock.Object, roleManagerMock.Object, stripeSettingsMock.Object);

            // Act

            // Call the Index method of the HomeController instance, which returns a ViewResult.
            var result = await controller.Index();

            // Assert

            // Assert that the result is a ViewResult.
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);

        // Assert that the ViewName property of the ViewResult is null.
        Xunit.Assert.Null(viewResult.ViewName);
        }*/

        
    }
}
