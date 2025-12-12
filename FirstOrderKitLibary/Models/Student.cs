using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class Student : Model
    {
        string studentId;
        int unitId;
        string studentNickName;
        string password;
        string studentFirstName;
        string studentLastName;
        int cityId;
        string studentTelephone;
        string studentAdrres;
        string studentImage;
        string studentSalt;
        public Student()
        {

        }
        public string StudentSalt { get; set; }
        public string StudentId
        {
            get { return this.studentId; }
            set { this.studentId = value; }
        }
        public int UnitId
        {
            get { return this.unitId; }
            set { this.unitId = value; }
        }
        [Required(ErrorMessage = "answer Text cannot be empty")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "studentNickName")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string StudentNickName
        {
            get { return this.studentNickName; }
            set { this.studentNickName = value;
                //ValidateProperty(value, "StudentNickName");
            }
        }
        [Required(ErrorMessage = "password Text cannot be empty")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "password")]
        public string Password
        {
            get { return this.password; }
            set { this.password = value;
                //ValidateProperty(value, "Password");
            }
        }
        [Required(ErrorMessage = "studentFirstName Text cannot be empty")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "studentFirstName")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string StudentFirstName
        {
            get { return this.studentFirstName; }
            set { this.studentFirstName = value;
                //ValidateProperty(value, "StudentFirstName");
            }
        }
        [Required(ErrorMessage = "studentLastName Text cannot be empty")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "studentLastName")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string StudentLastName
        {
            get { return this.studentLastName; }
            set { this.studentLastName = value;
                //ValidateProperty(value, "StudentLastName");
            }
        }
        public int CityId
        {
            get { return this.cityId; }
            set { this.cityId = value; }
        }
        [Required(ErrorMessage = "studentTelephone Text cannot be empty")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "StudentTelephone")]
        public string StudentTelephone
        {
            get { return this.studentTelephone; }
            set { this.studentTelephone = value;
                //ValidateProperty(value, "StudentTelephone");
            }
        }
        [Required(ErrorMessage = "studentAdrres Text cannot be empty")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "studentAdrres")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string StudentAdrres
        {
            get { return this.studentAdrres; }
            set { this.studentAdrres = value;
                //ValidateProperty(value, "StudentAdrres");
            }
        }
        [Required(ErrorMessage = "studentImage Text cannot be empty")]
        [OnlyImage(ErrorMessage = "The picture must be in image format (jpg, png, gif).")]
        public string StudentImage
        {
            get { return this.studentImage; }
            set { this.studentImage = value;
                //ValidateProperty(value, "StudentImage");
            }
        }
    }
   
}
