using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdScript
{
    class FlowConfig
    {
        public List<AnswerFlow> AnswerFlow { get; set; }

        public List<PhraseFlow> PhraseFlow { get; set; }

        public FlowConfig()
        {
            AnswerFlow = new List<AnswerFlow>();
            PhraseFlow = new List<PhraseFlow>();
        }
    }
}
