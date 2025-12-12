using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class Subject : Model
    {
        string subjectId;
        string subjectName;
        public Subject()
        {

        }
        public string SubjectId
        {
            get { return this.subjectId; }
            set { this.subjectId = value; }
        }
        [Required(ErrorMessage = "subjectName cannot be empty")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "subjectName")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string SubjectName
        {
            get { return this.subjectName; }
            set { this.subjectName = value;
                //ValidateProperty(value, "SubjectName");//באופן זמני
            }
        }
    }
}
