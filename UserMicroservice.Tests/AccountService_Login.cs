using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Entities;
using UserMicroservice.Data.Enums;
using UserMicroservice.Services;
using Xunit;

namespace UserMicroservice.Tests
{
    public class AccountService_Login
    {
        [Fact]
        public void Login_CorrectPassword_ReturnsJwt()
        {
            //arrange
            var model = new LoginDto() { Email = "test", Password = "test" };
            var jwt = new Jwt() { Token = "token"};
            User findResult = new User();
            var jwtFactoryMock = new Mock<IJwtFactory>();
            jwtFactoryMock.Setup(jf => jf.Create(It.IsAny<User>(), It.IsAny<int>())).Returns(jwt);
            var userManagerMock = MockUserManager<User>();
            userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(findResult);
            var signinManagerMock = MockSignInManager(userManagerMock);
            signinManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(It.IsAny<User>(), model.Password, It.IsAny<bool>()))
                             .ReturnsAsync(SignInResult.Success);

            var sut = new AccountService(signinManagerMock.Object, userManagerMock.Object, jwtFactoryMock.Object);

            //act
            var result = sut.Login(model).Result;

            //asset
            result.Jwt.Should().BeSameAs(jwt);
            result.UserExist.Should().BeTrue();
            result.PasswordCheck.Should().BeTrue();
            userManagerMock.Verify(um => um.FindByEmailAsync(It.IsAny<string>()), Times.Exactly(2));
            signinManagerMock.Verify(sm => sm.CheckPasswordSignInAsync(It.IsAny<User>(), model.Password, It.IsAny<bool>()), Times.Once);
            jwtFactoryMock.Verify(jf => jf.Create(It.IsAny<User>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Login_IncorrectPassword_JwtNotProduced()
        {
            //arrange
            var model = new LoginDto() { Email = "test", Password = "test" };
            User findResult = new User();
            var jwtFactoryMock = new Mock<IJwtFactory>();
            var userManagerMock = MockUserManager<User>();
            userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(findResult);
            var signinManagerMock = MockSignInManager(userManagerMock);
            signinManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(It.IsAny<User>(), model.Password, It.IsAny<bool>()))
                             .ReturnsAsync(SignInResult.Failed);

            var sut = new AccountService(signinManagerMock.Object, userManagerMock.Object, jwtFactoryMock.Object);

            //act
            var result = sut.Login(model).Result;

            //asset
            result.Jwt.Should().BeNull();
            result.UserExist.Should().BeTrue();
            result.PasswordCheck.Should().BeFalse();
            userManagerMock.Verify(um => um.FindByEmailAsync(It.IsAny<string>()), Times.Exactly(2));
            signinManagerMock.Verify(sm => sm.CheckPasswordSignInAsync(It.IsAny<User>(), model.Password, It.IsAny<bool>()), Times.Once);
            jwtFactoryMock.Verify(jf => jf.Create(It.IsAny<User>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Login_UserNotExist_JwtNotProduced()
        {
            //arrange
            var model = new LoginDto() { Email = "test", Password = "test" };
            User findResult = null;
            var jwtFactoryMock = new Mock<IJwtFactory>();
            var userManagerMock = MockUserManager<User>();
            userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(findResult);
            var signinManagerMock = MockSignInManager(userManagerMock);
            var sut = new AccountService(signinManagerMock.Object, userManagerMock.Object, jwtFactoryMock.Object);

            //act
            var result = sut.Login(model).Result;

            //asset
            result.Jwt.Should().BeNull();
            result.UserExist.Should().BeFalse();
            result.PasswordCheck.Should().BeFalse();
            userManagerMock.Verify(um => um.FindByEmailAsync(It.IsAny<string>()), Times.Exactly(1));
            signinManagerMock.Verify(sm => sm.CheckPasswordSignInAsync(It.IsAny<User>(), model.Password, It.IsAny<bool>()), Times.Never);
            jwtFactoryMock.Verify(jf => jf.Create(It.IsAny<User>(), It.IsAny<int>()), Times.Never);
        }

        private Mock<SignInManager<TUser>> MockSignInManager<TUser>(Mock<UserManager<TUser>> userManagerMock) where TUser : class
            => new Mock<SignInManager<TUser>>(userManagerMock.Object, Mock.Of<IHttpContextAccessor>(),
                            Mock.Of<IUserClaimsPrincipalFactory<TUser>>(), null, null, null, null);

        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
            => new Mock<UserManager<TUser>>(new Mock<IUserStore<TUser>>().Object, null, null, null, null, null, null, null, null);

    }
}
