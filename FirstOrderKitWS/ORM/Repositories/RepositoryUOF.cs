namespace FirstOrderKitWS.ORM.Repositories
{
    public class RepositoryUOF
    {
        AnswerRepository answerRepository;
        CityRepository cityRepository;
        MessageRepository messageRepository;
        QuestionRepository questionRepository;
        StudentRepository studentRepository;
        SubjectRepository subjectRepository;
        TestRepository testRepository;
        UnitRepository unitRepository;

        DBHelperOledb helperOledb;
        ModelCreaters modelCreaters;
        public RepositoryUOF()
        {
            this.helperOledb =new DBHelperOledb();
            this.modelCreaters = new ModelCreaters();
        }
        public DBHelperOledb DBHelperOledb
        {
            get
            {
                return this.helperOledb;
            }
        }

        public AnswerRepository AnswerRepository
        {
            get
            {
                if (this.answerRepository == null)
                    this.answerRepository = new AnswerRepository(this.helperOledb,this.modelCreaters);
                return this.answerRepository;
            }
        }
        public CityRepository CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                    this.cityRepository = new CityRepository(this.helperOledb, this.modelCreaters);
                return this.cityRepository;
            }
        }
        public MessageRepository MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                    this.messageRepository = new MessageRepository(this.helperOledb, this.modelCreaters);
                return this.messageRepository;
            }
        }
        public QuestionRepository QuestionRepository
        {
            get
            {
                if (this.questionRepository == null)
                    this.questionRepository = new QuestionRepository(this.helperOledb, this.modelCreaters);
                return this.questionRepository;
            }
        }
        public StudentRepository StudentRepository
        {
            get
            {
                if (this.studentRepository == null)
                    this.studentRepository = new StudentRepository(this.helperOledb, this.modelCreaters);
                return this.studentRepository;
            }
        }
        public SubjectRepository SubjectRepository
        {
            get
            {
                if (this.subjectRepository == null)
                    this.subjectRepository = new SubjectRepository(this.helperOledb, this.modelCreaters);
                return this.subjectRepository;
            }
        }
        public TestRepository TestRepository
        {
            get
            {
                if (this.testRepository == null)
                    this.testRepository = new TestRepository(this.helperOledb, this.modelCreaters);
                return this.testRepository;
            }
        }
        public UnitRepository UnitRepository
        {
            get
            {
                if (this.unitRepository == null)
                    this.unitRepository = new UnitRepository(this.helperOledb, this.modelCreaters);
                return this.unitRepository;
            }
        }
    }
}
