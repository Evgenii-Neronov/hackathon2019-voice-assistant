using System;
using System.Linq;
using System.Threading.Tasks;
using DialogFlow.Interfaces;
using DialogFlow.PriceEstimatorApi;

namespace DialogFlow.Model
{
    public class DialogFlow : IDialogFlow
    {
        public async Task<FlowContext> TalkAsync(string sentence, FlowContext flowContext)
        {
            flowContext = await TalkInternalAsync(sentence, flowContext);

            flowContext = VariableProcessor.ComputeVariables(flowContext);

            return flowContext;
        }

        private async Task<FlowContext> TalkInternalAsync(string sentence, FlowContext flowContext)
        {
            sentence = sentence.ToLower();

            if (flowContext.CurrentProcedure != null)
            {
                var answerFlow = flowContext.FlowConfig.GetAnswerFlow(flowContext.CurrentProcedure);

                var ua = answerFlow.GetNextAskSentence;

                if (ua?.AnswerVariable == "area")
                {
                    if (int.TryParse(sentence, out var area) == false)
                    {
                        flowContext.Answer = $"Ваш ответ не распознан как число. Назовите площадь в метрах.";

                        return flowContext;
                    }
                }

                if (ua?.AnswerVariable == "rooms")
                {
                    if (int.TryParse(sentence, out var rooms) == false)
                    {
                        flowContext.Answer = $"Ваш ответ не распознан как число. Назовите число - количество комнат. Например, 1, 2, 3 и т.д.";

                        return flowContext;
                    }
                }


                await this.ProcessProcedureAsync(sentence, flowContext);
            }
            else
            {
                foreach (var phraseFlow in flowContext.FlowConfig.PhraseFlow)
                {
                    foreach (var variant in phraseFlow.Variants)
                    {
                        var variantWords = variant.Trim().Split(' ');

                        foreach (var variantWord in variantWords)
                        {
                            var b = sentence.Contains(variantWord);

                            b = false;
                        }

                        if (variantWords.All(x => sentence.Contains(x)))
                        {
                            var answerFlow = flowContext.FlowConfig.GetAnswerFlow(phraseFlow.ProcedureName);

                            var nextAskSentence = answerFlow.GetNextAskSentence;

                            flowContext.Answer = nextAskSentence.Text;

                            if (nextAskSentence.AnswerVariable != null)
                                flowContext.CurrentProcedure = phraseFlow.ProcedureName;

                            return flowContext;
                        }
                    }
                }

                flowContext.Answer = "Спросите что-нибудь ещё";
            }

            return flowContext;
        }

        private async Task<FlowContext> ProcessProcedureAsync(string sentence, FlowContext flowContext)
        {
            if (sentence == "отмена")
            {
                flowContext.FlowConfig.ClearUserAnswers(flowContext.CurrentProcedure);
                flowContext.CurrentProcedure = null;
                flowContext.Answer = "Вы отменили опрос, если хотите, попробуйте ещё";

                return flowContext;
            }

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

                if (flowContext.CurrentProcedure == "price-estimate")
                {
                    try
                    {
                        await PriceEstimateHint(flowContext);
                    }
                    catch (Exception e)
                    {
                        flowContext.Answer = $"От компонента оценки квартир пришла ошибка: {e.Message}. Попробуйте повторить операцию позднее. ";
                        //flowContext.FlowConfig.ClearUserAnswers(flowContext.CurrentProcedure);
                        //flowContext.CurrentProcedure = null;
                    }
                    
                }

                flowContext.FlowConfig.ClearUserAnswers(flowContext.CurrentProcedure);

                flowContext.CurrentProcedure = null;
            }

            return flowContext;
        }

        private static async Task PriceEstimateHint(FlowContext flowContext)
        {
            var af = flowContext.FlowConfig.GetAnswerFlow(flowContext.CurrentProcedure);

            var response = await PriceEstimator.GetFlatPriceAsync2(new GetFlatPriceRequest()
            {
                address = af.GetAnswerValue("address"),
                area = int.Parse(af.GetAnswerValue("area")),
                roomsCount = int.Parse(af.GetAnswerValue("rooms")),
                filters = new Filter1[]
                {
                    new Filter1()
                    {
                        key = "entrance",
                        value = new[] {"entranceAfterRepair"}
                    },
                }
            });

            if (response.sale == null)
            {
                flowContext.Answer =
                    "По данному объекту нет информации. Попробуйте уточнить адрес или другие параметры";

                return;
            }

            var price = $"{response.sale.price / 1000000:N2}";

            flowContext.Answer = flowContext.Answer.Replace("#price", price);
            flowContext.Answer = flowContext.Answer.Replace("#rent", response.rent.price.ToString());
        }
    }
}
