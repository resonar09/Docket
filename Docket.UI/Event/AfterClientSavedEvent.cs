using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Event
{
    class AfterClientSavedEvent : PubSubEvent<AfterClientSavedEventArgs>
    {
    }

    internal class AfterClientSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }

    }
}
