using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FirstOrderKitModel
{
    public class Answer
    {
           string answerid;
           string trueFalse;
           string questionId;
           string answerText;

        public string Answerid
        { 
            get { return this. answerid; }
            set { this.answerid= value; } 
        }

        public string TrueFalse
        {
            get { return this.trueFalse; }
            set { this.trueFalse = value; }
        }
        public string QuestionId
        {
            get { return this.questionId; }
            set { this.questionId = value; }
        }
        [Required(ErrorMessage = "answer Text cannot be empty")]
        [StringLength(200,MinimumLength =5,ErrorMessage = "answerText")]
        [FirstLetterCapitalAttribute(ErrorMessage= "The first letter must be capitaliezed")]
        public string AnswerText
        {
            get { return this.answerText; }
            set { this.answerText = value; }
        }
       
    }
}
