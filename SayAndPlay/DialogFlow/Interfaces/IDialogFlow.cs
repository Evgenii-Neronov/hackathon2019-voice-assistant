using System.Threading.Tasks;
using DialogFlow.Model;

namespace DialogFlow.Interfaces
{
    public interface IDialogFlow
    {
        Task<FlowContext> TalkAsync(string sentence, FlowContext flowContext);
    }
}
