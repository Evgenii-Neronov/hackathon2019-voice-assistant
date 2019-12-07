using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogFlow.Interfaces;

namespace DialogFlow.Model
{
    public class DialogFlow : IDialogFlow
    {
        public FlowContext Talk(string sentence, FlowContext flowContext)
        {
            sentence = sentence.ToLower();

            if (flowContext.CurrentProcedure != null)
            {
                this.ProcessProcedure(sentence, flowContext);
            }
            else
            {
                foreach (var phraseFlow in flowContext.FlowConfig.PhraseFlow)
                {
                    foreach (var variant in phraseFlow.Variants)
                    {
                        if (variant.Split(' ').Any(x => sentence.Contains(x)))
                        {
                            flowContext.CurrentProcedure = phraseFlow.ProcedureName;

                            flowContext.Answer = flowContext.FlowConfig.GetAnswerFlow(phraseFlow.ProcedureName).GetNextAskSentence.Text;

                            return flowContext;
                        }
                    }
                }

                flowContext.Answer = "Спросите что-нибудь ещё";
            }

            return flowContext;
        }

        private void ProcessProcedure(string sentence, FlowContext flowContext)
        {
            var answerFlow = flowContext.FlowConfig.GetAnswerFlow(flowContext.CurrentProcedure);

            answerFlow.GetNextAskSentence.UserAnswer = sentence;

            var next = answerFlow.GetNextAskSentence;

            if (next != null)
            {
                flowContext.Answer = next.Text;
            }
            else
            {
                flowContext.Answer = answerFlow.Answer;

                flowContext.FlowConfig.ClearUserAnswers(flowContext.CurrentProcedure);

                flowContext.CurrentProcedure = null;
            }
        }
    }
}
