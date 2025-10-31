namespace FirstOrderKitWS
{
    public interface IRepository<T>
    {
        bool Create();//insert 
        bool Update();
        bool Delete();
        List<T> GetAll();
        T GetById(string id);


    }
}
