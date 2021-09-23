using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using Xunit;

namespace UnitTestApp.Tests
{
    public class UserResponseModelTests
    {
        [Theory]
        [InlineData("Email", "FirstName", "LastName")]
        [InlineData(null, "Null", "0")]
        [InlineData("", "", "")]
        public void Ctor_ShouldImplementParameters(string Email, string FirstName, string LastName)
        {
            //Act
            var actualApiModel = new UserResponseModel
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName
            };

            //Assert
            Assert.Equal(Email, actualApiModel.Email);
            Assert.Equal(FirstName, actualApiModel.FirstName);
            Assert.Equal(LastName, actualApiModel.LastName);
        }
    }
}
