using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserMicroservice.Controllers;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Enums;
using UserMicroservice.Services;
using Xunit;

namespace UserMicroservice.Tests
{
    public class AccountController_RegisterUser
    {
        [Fact]
        public void RegisterUser_UsernameTaken_BadRequest()
        {
            RegisterDto input = new RegisterDto() { Email = "test" };
            RegisterDto registerResult = null; 
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(asm => asm.Register(It.IsAny<RegisterDto>(), RoleType.User)).ReturnsAsync(registerResult);
            var sut = new AccountController(accountServiceMock.Object);

            var result = sut.RegisterUser(input).Result;

            result.Should().BeOfType<BadRequestResult>();
            var response = (BadRequestResult)result;
            response.StatusCode.Should().Be(400);
        }

        [Fact]
        public void RegisterUser_NewUsername_Created()
        {
            RegisterDto registerDto = new RegisterDto() { Email = "test" };
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(asm => asm.Register(It.IsAny<RegisterDto>(), RoleType.User)).ReturnsAsync(registerDto);
            var sut = new AccountController(accountServiceMock.Object);

            var response = sut.RegisterUser(registerDto).Result;

            response.Should().BeOfType<CreatedResult>();
            var result = (CreatedResult)response;
            result.StatusCode.Should().Be(201);
            result.Value.Should().Be(registerDto);
        }
    }
}
