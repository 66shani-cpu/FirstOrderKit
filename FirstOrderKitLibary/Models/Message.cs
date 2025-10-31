using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOrderKitModel
{
    public class Message:Model
    {
        string messageId;
        string messageName;
        public string MessageId
        {
            get { return this.messageId; }
            set { this.messageId = value; }
        }
        [Required(ErrorMessage = "MessageName Text cannot be empty")]
        [FirstLetterCapitalAttribute(ErrorMessage = "The first letter must be capitaliezed")]
        public string MessageName
        {
            get { return this.messageName; }
            set { this.messageName = value;
                ValidateProperty(value, "MessageName");
            }
        }

    }
}
