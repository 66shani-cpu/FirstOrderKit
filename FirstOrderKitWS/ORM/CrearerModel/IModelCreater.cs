using System.Data;

namespace FirstOrderKitWS
{
    public interface IModelCreater<T>
    {
        T CreateModel(IDataReader dataReader);

    }
}
