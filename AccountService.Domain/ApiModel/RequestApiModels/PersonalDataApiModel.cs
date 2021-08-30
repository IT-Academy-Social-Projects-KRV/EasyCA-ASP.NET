using AccountService.Data.Entities;
using System;
using System.Collections.Generic;

namespace AccountService.Domain.ApiModel.RequestApiModels
{
    public class PersonalDataApiModel
    {
        /// <summary>
        /// User Address
        /// </summary>     
        /// <example>Rivne</example>
        public Address UserAddress { get; set; }
        /// <summary>
        /// User ipn
        /// </summary>     
        /// <example>3652112697</example>
        public string IPN { get; set; }
        /// <summary>
        /// User serivceId if exist
        /// </summary>     
        /// <example>00000001</example>
        public string ServiceNumber { get; set; }
        /// <summary>
        /// User BirthDay
        /// </summary>     
        /// <example>3.10.2002</example>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// User JobPosition
        /// </summary>     
        /// <example>programmer</example>
        public string JobPosition { get; set; }
        /// <summary>
        /// User DriverLicense
        /// </summary>     
        /// <example>object DriverLicense</example>
        public DriverLicense UserDriverLicense { get; set; }
        /// <summary>
        /// User cars
        /// </summary>     
        /// <example>bmw, audi</example>
        public List<Transport> UserCars { get; set; }
    }
}
