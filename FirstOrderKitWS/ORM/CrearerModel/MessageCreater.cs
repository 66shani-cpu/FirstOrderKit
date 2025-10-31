using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS
{
    public class MessageCreater: IModelCreater<Message>
    {
        public Message CreateModel(IDataReader dataReader)
        {
            Message message = new Message();
            message.MessageId = Convert.ToString(dataReader["MessageId"]);
            message.MessageName= Convert.ToString(dataReader["MessageName"]);
            return message;
        }
    }
}
