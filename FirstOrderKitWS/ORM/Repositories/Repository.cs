namespace FirstOrderKitWS
{
    public class Repository
    {
        protected DBHelperOledb helperOledb;
        protected ModelCreaters modelCreaters;

        public Repository(DBHelperOledb helperOledb,ModelCreaters modelCreaters)
        {
            this.helperOledb= helperOledb;
            this.modelCreaters= modelCreaters;
        }

    }
}
