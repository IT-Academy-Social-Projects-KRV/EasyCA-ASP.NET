using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestApp.Tests.Unit.Domain.ApiModels.ResponseModels
{
    public class PersonalDataResponseModelTests
    {
        [Theory]
        [InlineData(null, "12341234", "123123123", "03.10.2002", "programmer",null,null)]
        public void Ctor_ShouldImplementParameters(Address address, string ipn, 
            string serviceNumber, DateTime birthDay, 
            string jobPosition, DriverLicense userDriverLicense, 
            List<string> userCars)
        {
            // Act

            var testObject = new PersonalDataResponseModel();
            //Assert

            Assert.Equal(address, testObject.Address);
            Assert.Equal(ipn, testObject.IPN);
            Assert.Equal(serviceNumber, testObject.ServiceNumber);
            Assert.Equal(birthDay, testObject.BirthDay);
            Assert.Equal(jobPosition, testObject.JobPosition);
            Assert.Equal(userDriverLicense, testObject.UserDriverLicense);
            Assert.Equal(userCars, testObject.UserCars);
        }
    }
}
