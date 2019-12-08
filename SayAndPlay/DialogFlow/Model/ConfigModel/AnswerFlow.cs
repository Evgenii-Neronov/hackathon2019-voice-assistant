using System.Collections.Generic;
using System.Linq;

namespace DialogFlow.Model.ConfigModel
{
    public class AnswerFlow
    {
        public string ProcedureName { get; set; }

        public List<AskSentence> AskSentences { get; set; }

        public string Answer { get; set; }

        public List<Answer> Answers => AskSentences.Select(x => new Answer(x.AnswerVariable, x.UserAnswer)).ToList();

        public bool IsDone => AskSentences.Any(x => x.UserAnswer != null);

        public AskSentence GetNextAskSentence => AskSentences.FirstOrDefault(x => x.UserAnswer == null);

        public string GetAnswerValue(string answerVariable)
        {
            return Answers.FirstOrDefault(x => x.Variable == answerVariable).Value;
        }

        public AnswerFlow()
        {
            AskSentences = new List<AskSentence>();
        }
    }

    public class AskSentence
    {
        public string Text { get; set; }

        public string AnswerVariable { get; set; }

        public string UserAnswer { get; set; }
    }

    public class Answer
    {
        public Answer(string variable, string value)
        {
            this.Variable = variable;
            this.Value = value;
        }

        public Answer()
        {
            
        }

        public string Variable { get; set; }

        public string Value { get; set; }
    }
}
