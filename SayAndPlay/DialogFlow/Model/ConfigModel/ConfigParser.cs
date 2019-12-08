using System;
using System.Collections.Generic;
using System.Linq;

namespace DialogFlow.Model.ConfigModel
{
    public class ConfigParser
    {
        public static FlowConfig Parse(string configText)
        {
            var flowConfig = new FlowConfig();

            AnswerFlow currentAnswerFlow = null;

            configText = configText.ToLower();

            foreach (var line in SplitLinesAndTrim(configText))
            {
                if (line.Length == 0)
                    continue;

                if (line[0] != '[' && currentAnswerFlow == null)
                {
                    flowConfig.PhraseFlow.Add(new PhraseFlow()
                    {
                        ProcedureName = line.Split('>')[1].Trim(),
                        Variants = line.Split('>')[0].Split('|').ToList()
                    });
                }
                else
                {
                    if (line[0] == '[')
                    {
                        if (currentAnswerFlow != null)
                            flowConfig.AnswerFlow.Add(currentAnswerFlow);

                        currentAnswerFlow = new AnswerFlow()
                        {
                            ProcedureName = line.Replace("[", "").Replace("]", "")
                        };
                    }
                    else
                    {
                        if (line[0] != '>')
                        {
                            if (line.Contains("$"))
                            {
                                currentAnswerFlow.AskSentences.Add(new AskSentence()
                                {
                                    Text = line.Split('$')[0],
                                    AnswerVariable = line.Split('$')[1]
                                });
                            }
                            else
                            {
                                currentAnswerFlow.AskSentences.Add(new AskSentence()
                                {
                                    Text = line,
                                });
                            }
                        }
                        else
                        {
                            currentAnswerFlow.Answer = line.Replace(">", "");
                        }
                    }
                }
            }
            flowConfig.AnswerFlow.Add(currentAnswerFlow);

            return flowConfig;
        }

        private static List<string> SplitLinesAndTrim(string text)
        {
            return text.Split(Environment.NewLine.ToCharArray()).Select(x => x.Trim()).ToList();
        }
    }
}