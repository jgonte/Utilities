using System;

namespace Utilities
{
    public class LogMessage
    {
        public enum Levels
        {
            Information,
            Warning,
            Error
        }

        public Levels Level { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// The name of the class that logs the messages
        /// </summary>
        public string Owner { get; set; }

        public override string ToString()
        {
            return $"[{Owner}] [{Timestamp.ToString("G")}] - {Level}: {Message}";
        }
    }
}
