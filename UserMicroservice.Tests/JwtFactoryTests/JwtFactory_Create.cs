using FluentAssertions;
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
    public class JwtFactory_Create
    {

        [Theory]
        [InlineData(RoleType.Admin , 60)]
        [InlineData(RoleType.User , 150)]
        public void Create_AnyRoleAndAnyExpirationTime_WorksSuccessfully(RoleType role , int minutes)
        {
            //arrange
            User user = new User() { UserName = "testUserName", Email = "testEmail" };
            var key = "b3ff24525cbdb417dd1f7cbe8c37477d7393f905c873718b8a5222e43testkey";
            var audience = "testAudience";
            var issuer = "testIssuer";
            var rolesList = new List<string>() { role.ToString() };
            var userManagerMock = MockUserManager<User>();
            userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(rolesList);
            var sut = new JwtFactory(userManagerMock.Object, key, issuer, audience);

            //act
            var result = sut.Create(user, minutes);

            //asset
            result.Should().NotBeNull();
            result.Should().BeOfType<Jwt>();
            result.Expiration.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(minutes) , precision:60000);
            result.Roles.Should().Contain(role.ToString());
            result.Token.Length.Should().BeGreaterThan(10);
            userManagerMock.Verify(um => um.GetRolesAsync(user), Times.Once);
        }


        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
            => new Mock<UserManager<TUser>>(new Mock<IUserStore<TUser>>().Object, null, null, null, null, null, null, null, null);

    }
}
