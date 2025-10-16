using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class Unit
    {
        string unitId;
        string unitName;
        string unitPicture;
        public string UnitId
        {
            get { return this.unitId; }
            set { this.unitId = value; }
        }
        [Required(ErrorMessage = "unitName Text cannot be empty")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "unitName")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string UnitName
        {
            get { return this.unitName; }
            set { this.unitName = value; }
        }
        [Required(ErrorMessage = "studentImage Text cannot be empty")]
        [OnlyImage(ErrorMessage = "The picture must be in image format (jpg, png, gif).")]
        public string UnitPicture
        {
            get { return this.unitPicture; }
            set { this.unitPicture = value; }
        }
    }
}
