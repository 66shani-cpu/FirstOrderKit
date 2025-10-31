using FirstOrderKitModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FirstOrderKitModel
{
    public class OrderFirstKitViewModel
    {
        public List<Test> Tests { get; set; }
        public List<Unit> Units { get; set; }
        public List<Unit> Subject { get; set; }
        public int? Difficult { get; set; }// לקלוט רמה של קושי
        public string? SubjectId { get; set; }//לקלוט נושא נבחר


    }
}
