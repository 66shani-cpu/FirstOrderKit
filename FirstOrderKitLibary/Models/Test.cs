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
        string unitId;
        string levelQuestion;
        string studentId;
        string grade;
        public Test()
        {

        }
        public string TestId
        {
            get { return this.testId; }
            set { this.testId = value; }
        }
        [Required(ErrorMessage = "TestName Text cannot be empty")]
        //[StringLength(10, MinimumLength = 3, ErrorMessage = "TestName")]
        //[FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
       public  string TestName
        {
            get { return this.testName; }
            set { this.testName = value;
                //ValidateProperty(value, "testName");//להחזיר זה זמני
            }
        }
        public string UnitId
        {
            get { return this.unitId; }
            set {  this.unitId = value; }
        }
        public string LevelQuestion
        {
            get { return this.levelQuestion; }
            set { this.levelQuestion = value; }
        }
        public string StudentId
        {
            get { return this.studentId; }
            set { this.studentId = value; }
        }
        public string Grade
        {
            get { return this.grade; }
            set { this.grade = value; }
        }

    }
}
