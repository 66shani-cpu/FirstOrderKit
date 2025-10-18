using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class City:Model
    {
        string cityId;
        string cityName;
        string CityId
        {
            get { return this.cityId; }
            set { this.cityId = value; }
        }
        [Required(ErrorMessage = "City cannot be empty")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "CityName")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        string CityName
        {
            get { return this.cityName; }
            set { this.cityName = value;
                ValidateProperty(value, "CityName");
            }
        }
    }
}
