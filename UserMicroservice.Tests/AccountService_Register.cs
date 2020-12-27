using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Entities;
using UserMicroservice.Data.Enums;
using UserMicroservice.Services;
using Xunit;

namespace UserMicroservice.Tests
{
    public class AccountService_Register
    {
        [Theory]
        [InlineData(RoleType.Admin)]
        [InlineData(RoleType.User)]
        public void Register_EmailIsFree_WorksSuccessfully(RoleType role)
        {
            //arrange
            var model = new RegisterDto() { FirstName = "test", LastName = "test", Email = "test" };
            User findResult = null;
            var jwtFactoryMock = new Mock<IJwtFactory>();
            var userManagerMock = MockUserManager<User>();
            userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(findResult);
            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var signinManagerMock = MockSignInManager(userManagerMock);

            var sut = new AccountService(signinManagerMock.Object, userManagerMock.Object, jwtFactoryMock.Object);

            //act
            var actresult = sut.Register(model, role).Result;

            //asset
            actresult.Should().NotBeNull();
            actresult.Should().BeOfType<RegisterDto>();
            userManagerMock.Verify(um => um.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>() , It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>() , role.ToString()), Times.Once);
        }

        [Theory]
        [InlineData(RoleType.Admin)]
        [InlineData(RoleType.User)]
        public void Register_EmailIsBuisy_ReturnsNull(RoleType role)
        {
            //arrange
            var model = new RegisterDto() { FirstName = "test", LastName = "test", Email = "test" };
            User findResult = new User();
            var jwtFactoryMock = new Mock<IJwtFactory>();
            var userManagerMock = MockUserManager<User>();
            userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(findResult);
            var signinManagerMock = MockSignInManager(userManagerMock);
           
            var sut = new AccountService(signinManagerMock.Object, userManagerMock.Object, jwtFactoryMock.Object);

            //act
            var actresult = sut.Register(model, role).Result;

            //asset
            actresult.Should().BeNull();
            userManagerMock.Verify(um => um.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), role.ToString()), Times.Never);
        }

        private Mock<SignInManager<TUser>> MockSignInManager<TUser>(Mock<UserManager<TUser>> userManagerMock) where TUser : class
            =>new Mock<SignInManager<TUser>>(userManagerMock.Object, Mock.Of<IHttpContextAccessor>(),
                            Mock.Of<IUserClaimsPrincipalFactory<TUser>>(), null, null, null, null);
     
        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
            => new Mock<UserManager<TUser>>(new Mock<IUserStore<TUser>>().Object, null, null, null, null, null, null, null, null);

    }
}
