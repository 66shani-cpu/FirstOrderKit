using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class AddQuestionViewModel:Question
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
