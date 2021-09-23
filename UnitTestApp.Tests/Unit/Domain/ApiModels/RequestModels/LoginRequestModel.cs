using AuthMicroservice.Domain.ApiModel.RequestApiModels;
using Xunit;

namespace UnitTestApp.Tests.Unit.Domain.ApiModels.RequestModels
{
    public class LoginRequestModel
    {
        [Theory]
        [InlineData("Email", "Password")]
        [InlineData("qwerty@gmail.com", "Qwerty211@")]
        [InlineData("", "")]
        public void Ctor_ShouldImplementParameters(string Email, string Password)
        {
            //Act
            var actualApiModel = new LoginApiModel
            {
                Email = Email,
                Password = Password,
            };

            //Assert
            Assert.Equal(Email, actualApiModel.Email);
            Assert.Equal(Password, actualApiModel.Password);
        }
    }
}
