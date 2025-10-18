using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class Questions:Model
    {
        string questionId;
        string levelQuestions;
        string question;
        public string QuestionId
        {
            get { return this.questionId; }
            set { this.questionId = value; }
        }
        [Required(ErrorMessage = "LevelQuestions cannot be empty")]
        public string LevelQuestions
        {
            get { return this.levelQuestions; }
            set { this.levelQuestions = value;
                ValidateProperty(value, "LevelQuestions");
            }
        }
        [Required(ErrorMessage = "question cannot be empty")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "question")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string Question
        {
            get { return this.question; }
            set { this.question = value;
                ValidateProperty(value, "question");
            }
        }
    }
}
