using FluentValidation;
using System.Text.RegularExpressions;
using CrudMicroservice.Data.Entities;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    public class InsuaranceValidator:AbstractValidator<Insuarance>
    {
        public InsuaranceValidator()
        {
            RuleFor(n => n.CompanyName).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("Isuarance company is required").Length(4, 35).
                WithMessage("Number of characters is not valid").Must(IsValidCompany).
                WithMessage("This cmpany is not valid member of MTSBU");
            RuleFor(n => n.SerialNumber).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("Serial number is required").Length(9).
                WithMessage("Wrong number of characters").Must(IsValidSerial).
                WithMessage("Wrong format of insuarance serial");
        }
        public static bool IsValidCompany(string company)
        {
            List<string> validCompanies = new List<string>() 
            {
                //I think we have problems, maybe we must also use this company names in English ?
                "СК Універсальна", "НАСК ОРАНТА", "УАСК АСКА", "СК ГРАВЕ УКРАЇНА",
                "СК ІНГО", "КНЯЖА ВІЄННА ІНШУРАНС ГРУП", "СТ Гарантія", "СК Євроінс Україна",
                "АСКО-Донбас Північний", "ОМЕГА", "Скарбниця", "УПСК", "Оранта-Січ", "УТСК",
                "ПЗУ Україна", "ПРОВІДНА", "ІНТЕР-ПОЛІС", "ПРОСТО-страхування", "Колоннейд Україна",
                "ТАС", "УНІКА", "АРКС", "Європейський страховий альянс", "УСГ", "МОТОР-ГАРАНТ",
                "ЕТАЛОН","ВАН КЛІК", "МЕГА-ГАРАНТ","КРЕДО","КРАЇНА", "ВУСО", "БРОКБІЗНЕС", "Альфа-Гарант",
                "АЛЬФА СТРАХУВАННЯ", "САЛАМАНДРА", "Український страховий стандарт", "ПЕРША", "ЮНІВЕС",
                "АРСЕНАЛ СТРАХУВАННЯ", "Ю.БІ.АЙ - КООП", "Експрес Страхування", "ГАРДІАН", "ОБЕРІГ"
            };
            
            if (validCompanies.Contains(company))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidSerial(string serial)
        {
            Regex insuaranceSerial = new Regex(@"[А-ЩЬЮЯЇІЄҐA-Z]{2}[0-9]{7}");

            if (insuaranceSerial.IsMatch(serial))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
