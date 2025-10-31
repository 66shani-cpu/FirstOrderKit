namespace FirstOrderKitWS
{
    public class Repository
    {
        protected DBHelperOledb helperOledb;
        protected ModelCreaters modelCreaters;

        public Repository()
        {
            this.helperOledb=new DBHelperOledb();
            this.modelCreaters=new ModelCreaters();
        }

    }
}
