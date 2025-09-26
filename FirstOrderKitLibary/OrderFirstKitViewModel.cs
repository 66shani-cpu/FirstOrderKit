using FirstOrderKitLibary;
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
        public List<Answer> Answers { get; set; }
        public List<Test> Tests { get; set; }
        public List<Unit> Units { get; set; }

    }
}
