using AuthMicroservice.Domain.ApiModel.ResponseApiModels;
using Xunit;

namespace UnitTestApp.Tests.Unit.Domain.ApiModels.ResponseModels
{
    public class AuthenticateResponseApiModelTests
    {
        [Theory]
        [InlineData("email","token", "refreshToken", "role")]
        public void Ctor_ShouldImplementParameters(string email, string token, string refreshToken = null, string role = "Admin")
        {
            // Act
            var testObject = new AuthenticateResponseApiModel(email, token, refreshToken,role);
            //Assert
            Assert.Equal(token, testObject.Token);
            Assert.Equal(email, testObject.Email);
            Assert.Equal(role, testObject.Role);
            Assert.Equal(refreshToken, testObject.RefreshToken);
        }
    }
}
