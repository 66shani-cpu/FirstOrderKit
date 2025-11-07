using FirstOrderKitModel;
using System.Data;

namespace FirstOrderKitWS.ORM.Repositories
{
    public class MessageRepository : Repository, IRepository<Message>
    {
        public bool Create(Message model)
        {
            string sql = @$"Insert into Message 
                           (MessageText)
                          values(@MessageText)";
            this.helperOledb.AddParameter("@MessageText", model.MessageName);
            return this.helperOledb.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = @"Delete from Message where MessageId=@MessageId";
            this.helperOledb.AddParameter("@MessageId", id);
            return this.helperOledb.Delete(sql) > 0;
        }
        //מעביר את הטבלה למודל
        public List<Message> GetAll()
        {
            string sql = " Select * from Message";

            List<Message> messages = new List<Message>();
            //אחרי שימוש ברידר למחוק אותו בזיכרון במחשב כדי שלא יהיה הרבה זבל
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                while (reader.Read())
                {
                    messages.Add(this.modelCreaters.MessageCreater.CreateModel(reader));
                }
            }

            return messages;
        }

        public Message GetById(string id)
        {
            string sql = " Select * from Message  where MessageId=@MessageId";
            this.helperOledb.AddParameter("@MessageId", id);
            using (IDataReader reader = this.helperOledb.Select(sql))
            {
                reader.Read();
                return this.modelCreaters.MessageCreater.CreateModel(reader);
            }
        }

        public bool Update(Message model)
        {
            string sql = @"Update Message set MessageText=@MessageText";
            this.helperOledb.AddParameter("@MessageText", model.MessageName);
            return this.helperOledb.Update(sql) > 0;
        }
    }
}
