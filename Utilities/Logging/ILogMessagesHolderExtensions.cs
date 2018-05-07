using System;

namespace Utilities
{
    public static class ILogMessagesHolderExtensions
    {
        public static void Information<T>(this T holder, string message) where T : ILogMessagesHolder
        {
            AddMessage(holder, LogMessage.Levels.Information, message);
        }

        public static void Warning<T>(this T holder, string message) where T : ILogMessagesHolder
        {
            AddMessage(holder, LogMessage.Levels.Warning, message);
        }

        public static void Error<T>(this T holder, string message) where T : ILogMessagesHolder
        {
            AddMessage(holder, LogMessage.Levels.Error, message);
        }

        private static void AddMessage<T>(T holder, LogMessage.Levels type, string message) where T : ILogMessagesHolder
        {
            holder.LogMessages.Add(new LogMessage
            {
                Owner = typeof(T).FullName,
                Level = type,
                Timestamp = DateTime.UtcNow,
                Message = message
            });
        }
    }
}
