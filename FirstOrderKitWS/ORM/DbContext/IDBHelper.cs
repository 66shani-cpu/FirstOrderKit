using System.Data;

namespace FirstOrderKitWS
{
    public interface IDBHelper
    {
        void OpenConnection();
        void CloseConnection();
        //dataReader אובייקט של recordset
        //פעולות CRUD
        IDataReader Select(string sql);
        int Update(string sql);
        int Insert(string sql);
        int Delete(string sql);
        void OpenTransaction();
        void Commit();
        void Rollback();

    }
}
