using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserMicroservice.Controllers;
using UserMicroservice.Data.Dto;
using UserMicroservice.Services;
using Xunit;

namespace UserMicroservice.Tests
{
    public class AccountController_Login
    {
        [Fact]
        public void Login_CorrectData_OkObjectAndJwt()
        {
            var model = new LoginDto() { Email = "test" };
            var jwt = new Jwt() { Token = "token" };
            var loginResult = new LoginResult() { Jwt = jwt, PasswordCheck = true, UserExist = true };
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(asm => asm.Login(model)).ReturnsAsync(loginResult);
            var sut = new AccountController(accountServiceMock.Object);

            var response = sut.Login(model).Result;

            response.Should().BeOfType<OkObjectResult>();
            var result = (OkObjectResult)response;
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(jwt);
        }

        [Fact]
        public void Login_IncorrectPassword_BadRequest()
        {
            var model = new LoginDto() { Email = "test" };
            var jwt = new Jwt() { Token = "token" };
            var loginResult = new LoginResult() { Jwt = null, PasswordCheck = false, UserExist = true };
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(asm => asm.Login(model)).ReturnsAsync(loginResult);
            var sut = new AccountController(accountServiceMock.Object);

            var result = sut.Login(model).Result;

            result.Should().BeOfType<BadRequestResult>();
            var response = (BadRequestResult)result;
            response.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Login_UserNotExist_Unauthorized()
        {
            var model = new LoginDto() { Email = "test" };
            var jwt = new Jwt() { Token = "token" };
            var loginResult = new LoginResult() { Jwt = null, PasswordCheck = false, UserExist = false };
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(asm => asm.Login(model)).ReturnsAsync(loginResult);
            var sut = new AccountController(accountServiceMock.Object);

            var result = sut.Login(model).Result;

            result.Should().BeOfType<UnauthorizedResult>();
            var response = (UnauthorizedResult)result;
            response.StatusCode.Should().Be(401);
        }
    }
}
