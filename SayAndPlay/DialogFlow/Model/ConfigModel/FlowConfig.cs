using System.Collections.Generic;
using System.Linq;

namespace DialogFlow.Model.ConfigModel
{
    public class FlowConfig
    {
        public List<AnswerFlow> AnswerFlow { get; set; }

        public List<PhraseFlow> PhraseFlow { get; set; }

        public AnswerFlow GetAnswerFlow(string procedureName)
        {
            return AnswerFlow.FirstOrDefault(x => x.ProcedureName == procedureName);
        }

        public void ClearUserAnswers(string procedureName)
        {
            foreach (var askSentence in GetAnswerFlow(procedureName).AskSentences)
            {
                askSentence.UserAnswer = null;
            }
        }

        public FlowConfig()
        {
            AnswerFlow = new List<AnswerFlow>();
            PhraseFlow = new List<PhraseFlow>();
        }
    }
}
