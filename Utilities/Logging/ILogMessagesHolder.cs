using System.Collections.Generic;

namespace Utilities
{
    public interface ILogMessagesHolder
    {
        List<LogMessage> LogMessages { get; set; }
    }
}
