using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class RequestANewTestViewModel
    {
        public List<Unit> Units { get; set; }
        public List<Unit> Subject { get; set; }
        public int? Difficult { get; set; }// לקלוט רמה של קושי
        public string? SubjectId { get; set; }//לקלוט נושא נבחר
        public string? UnitId { get; set; }//לקלוט יחידה נבחרת
    }
}
