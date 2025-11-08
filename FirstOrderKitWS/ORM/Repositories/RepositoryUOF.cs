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
        public AnswerRepository AnswerRepository
        {
            get
            {
                if (this.answerRepository == null)
                    this.answerRepository = new AnswerRepository();
                return this.answerRepository;
            }
        }
        public CityRepository CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                    this.cityRepository = new CityRepository();
                return this.cityRepository;
            }
        }
        public MessageRepository MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                    this.messageRepository = new MessageRepository();
                return this.messageRepository;
            }
        }
        public QuestionRepository QuestionRepository
        {
            get
            {
                if (this.questionRepository == null)
                    this.questionRepository = new QuestionRepository();
                return this.questionRepository;
            }
        }
        public StudentRepository StudentRepository
        {
            get
            {
                if (this.studentRepository == null)
                    this.studentRepository = new StudentRepository();
                return this.studentRepository;
            }
        }
        public SubjectRepository SubjectRepository
        {
            get
            {
                if (this.subjectRepository == null)
                    this.subjectRepository = new SubjectRepository();
                return this.subjectRepository;
            }
        }
        public TestRepository TestRepository
        {
            get
            {
                if (this.testRepository == null)
                    this.testRepository = new TestRepository();
                return this.testRepository;
            }
        }
        public UnitRepository UnitRepository
        {
            get
            {
                if (this.unitRepository == null)
                    this.unitRepository = new UnitRepository();
                return this.unitRepository;
            }
        }
    }
}
