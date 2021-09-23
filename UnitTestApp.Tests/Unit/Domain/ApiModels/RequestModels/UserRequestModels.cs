using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using Xunit;

namespace UnitTestApp.Tests
{
    public class UserApiModelTests
    {
        [Theory]
        [InlineData("Email", "FirstName", "LastName", null)]
        [InlineData(null, "Null", "0", null)]
        [InlineData("", "", "", null)]
        public void Ctor_ShouldImplementParameters(string Email, string FirstName, string LastName, PersonalDataRequestModel Data)
        {
            //Act
            var actualApiModel = new UserRequestModel
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PersonalData = Data
            };

            //Assert
            Assert.Equal(Email, actualApiModel.Email);
            Assert.Equal(FirstName, actualApiModel.FirstName);
            Assert.Equal(LastName, actualApiModel.LastName);
            Assert.Equal(Data, actualApiModel.PersonalData);

        }
    }
}
