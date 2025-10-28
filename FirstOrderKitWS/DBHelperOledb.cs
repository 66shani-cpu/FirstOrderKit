using System.Data;
using System.Data.OleDb;


namespace FirstOrderKitWS
{
 
    public class DBHelperOledb : IDBHelper
    {
        //אחראי ליצור קשר עם מסד נתונים
        OleDbConnection OleDBConnection;
        //אחראי לשלוח משפטי sql למסד נתונים ומחזיר תשובה
        OleDbCommand dbCommand;
        //כמה שינויים במסד אחראי
        OleDbTransaction dbTransaction;
        public DBHelperOledb()
        {
            this.OleDBConnection = new OleDbConnection();
            //הסבר לאיזה מסד נתונים יש ליצור
            //this.OleDBConnection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Directory.GetCurrentDirectory()}\App_Data\FirstOrderKit.accdb"";Persist Security Info=True";
            this.OleDBConnection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""C:\Users\Shani\Desktop\פרוייקט הנדסת תוכנה יב\VSProject\FirstOrderKit\FirstOrderKitWS\App_Data\FirstOrderKit.accdb"";Persist Security Info=True";

            this.dbCommand = new OleDbCommand();
            this.dbCommand.Connection = this.OleDBConnection;

        }
        public void CloseConnection()
        {
            this.OleDBConnection.Close();

        }

        public void Commit()
        {
            this.dbTransaction.Commit();
        }

        public int Delete(string sql)
        {
            this.dbCommand.CommandText=sql;
            return this.dbCommand.ExecuteNonQuery();
          
        }
        
        public int Insert(string sql)
        {
            this.dbCommand.CommandText = sql;
            return this.dbCommand.ExecuteNonQuery();
        }

        public void OpenConnection()
        {
            this.OleDBConnection.Open();
        }
        
        public void OpenTransaction()
        {
            this.dbTransaction = this.OleDBConnection.BeginTransaction();
        }

        public void Rollback()
        {
          this.dbTransaction.Rollback();
        }

        public IDataReader Select(string sql)
        {
            this.dbCommand.CommandText = sql;
            return this.dbCommand.ExecuteReader();
        }

        public int Update(string sql)
        {
            this.dbCommand.CommandText = sql;
            return this.dbCommand.ExecuteNonQuery();
        }
    }
}
