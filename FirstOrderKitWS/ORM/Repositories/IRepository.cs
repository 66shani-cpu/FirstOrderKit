namespace FirstOrderKitWS
{
    public interface IRepository<T>
    {
        bool Create(T model);//insert 
        bool Update(T model);
        bool Delete(string id);
        List<T> GetAll();
        T GetById(string id);


    }
}
