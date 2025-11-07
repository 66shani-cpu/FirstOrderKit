namespace FirstOrderKitWS
{
    public class ModelCreaters
    {
        AnswerCreator answerCreator;
        CityCreater cityCreater;
        MessageCreater messageCreater;
        QuestionCreater questionCreater;
        StudentCreator studentCreator;
        SubjectCreater subjectCreater;
        TestCreater testCreater;
        UnitCreater unitCreater;

        public AnswerCreator AnswerCreator
        {
            get
            {
                if (this.answerCreator == null)
                    this.answerCreator = new AnswerCreator();
                return this.answerCreator;
            }
         }
        public MessageCreater MessageCreater
        {
            get
            {
                if (this.messageCreater == null)
                    this.messageCreater = new MessageCreater();
                return this.messageCreater;
            }
        }
        public CityCreater CityCreater
        {
            get
            {
                if (this.cityCreater == null)
                    this.cityCreater = new CityCreater();
                return this.cityCreater;
            }
        }
        public QuestionCreater QuestionCreater
        {
            get
            {
                if (this.questionCreater == null)
                    this.questionCreater = new QuestionCreater();
                return this.questionCreater;
            }
        }
        public StudentCreator StudentCreator
        {
            get
            {
                if (this.studentCreator == null)
                    this.studentCreator = new StudentCreator();
                return this.studentCreator;
            }
        }
        public SubjectCreater SubjectCreater
        {
            get
            {
                if (this.subjectCreater == null)
                    this.subjectCreater = new SubjectCreater();
                return this.subjectCreater;
            }
        }
        public TestCreater TestCreater
        {
            get
            {
                if (this.testCreater == null)
                    this.testCreater = new TestCreater();
                return this.testCreater;
            }
        }
        public UnitCreater UnitCreater
        {
            get
            {
                if (this.unitCreater == null)
                    this.unitCreater = new UnitCreater();
                return this.unitCreater;
            }
        }


    }
}
