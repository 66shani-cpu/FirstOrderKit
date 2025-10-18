using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class Test:Model
    {
        string testId;
        string testName;
        string TestId
        {
            get { return this.testId; }
            set { this.testId = value; }
        }
        [Required(ErrorMessage = "TestName Text cannot be empty")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "TestName")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        string TestName
        {
            get { return this.testName; }
            set { this.testName = value;
                ValidateProperty(value, "testName");
            }
        }
    }
}
