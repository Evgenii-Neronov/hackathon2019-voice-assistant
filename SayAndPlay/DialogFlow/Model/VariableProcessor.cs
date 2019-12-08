using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogFlow.Model
{
    public class VariableProcessor
    {
        public static FlowContext ComputeVariables(FlowContext flowContext)
        {
            var ans = flowContext.Answer;

            ans = ans.Replace("#hours", DateTime.Now.Hour.ToString());
            ans = ans.Replace("#minutes", DateTime.Now.Minute.ToString());
            ans = ans.Replace("#seconds", DateTime.Now.Second.ToString());

            ans = ans.Replace("#day", DateTime.Now.Day.ToString());
            ans = ans.Replace("#month", DateTime.Now.Month.ToString());
            ans = ans.Replace("#year", DateTime.Now.Year.ToString());


            flowContext.Answer = ans;


            return flowContext;
        }
    }
}
