using DialogFlow.Model.ConfigModel;

namespace DialogFlow.Model
{
    public class FlowContext
    {
        public PropertyContainer PropertyContainer { get; set; }

        public FlowConfig FlowConfig { get; set; }

        public string Answer { get; set; }

        public string CurrentProcedure { get; set; }

        public FlowContext(string configText)
        {
            FlowConfig = ConfigParser.Parse(configText);
        }
    }
}
