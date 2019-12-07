using System.Collections.Generic;

namespace DialogFlow.Model
{
    public class PropertyContainer
    {
        private Dictionary<string, string> properties;

        public string Get(string propertyName)
        {
            properties.TryGetValue(propertyName, out var value);
            return value;
        }

        public void Set(string properityName, string value)
        {
            properties[properityName] = value;
        }

        public PropertyContainer()
        {
            properties = new Dictionary<string, string>();
        }
    }
}
