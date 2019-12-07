using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdScript
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var configTxt = File.ReadAllText(@"C:\Users\Public\Documents\SayAndPlay\DialogFlow.config");

            var flowConfig = new FlowConfig();

            AnswerFlow currentAnswerFlow = null;

            foreach (var line in SplitLinesAndTrim(configTxt))
            {
                if(line.Length == 0)
                    continue;

                if (line[0] != '[' && currentAnswerFlow == null)
                {
                    flowConfig.PhraseFlow.Add(new PhraseFlow()
                    {
                        ProcedureName = line.Split('>')[1],
                        Variants = line.Split('>')[0].Split('|').ToList()
                    });
                }
                else
                {
                    if (line[0] == '[')
                    {
                        currentAnswerFlow = new AnswerFlow()
                        {
                            ProcedureName = line.Replace("[", "").Replace("]", "")
                        };
                    }
                    else
                    {
                        if (line[0] != '>')
                        {
                            if (line.Contains("&"))
                            {
                                currentAnswerFlow.AskSentences.Add(new AskSentence()
                                {
                                    Text = line.Split('&')[0],
                                    AnswerVariable = line.Split('&')[1]
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

                Console.WriteLine(line);
            }



            Console.WriteLine("hello word");
        }

        private static List<string> SplitLinesAndTrim(string text)
        {
            return text.Split(Environment.NewLine.ToCharArray()).Select(x => x.Trim()).ToList();
        }
    }
}
