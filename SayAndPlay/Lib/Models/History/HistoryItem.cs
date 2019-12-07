using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models.History
{
    public enum Who
    {
        Assistent,
        User,
    }
    public class HistoryItem
    {
        public Who  Who { get; set; }

        public string What { get; set; }

        public DateTime When { get; set; }

        public HistoryItem(Who who, string what)
        {
            this.Who = who;
            this.What = what;
            this.When = DateTime.Now;
        }
    }
}
