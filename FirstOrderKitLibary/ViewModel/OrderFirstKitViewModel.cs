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
        public List<Unit> Units { get; set; }
        public List<Subject> Subject { get; set; }
        public string? SubjectId { get; set; }//לקלוט נושא נבחר


    }
}
