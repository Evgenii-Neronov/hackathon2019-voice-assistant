using DialogFlow.Model;

namespace DialogFlow.Interfaces
{
    public interface IDialogFlow
    {
        FlowContext Talk(string sentence, FlowContext flowContext);
    }
}
