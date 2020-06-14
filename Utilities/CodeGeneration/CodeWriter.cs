using System;
using System.IO;

namespace CodeGeneration
{
    public abstract class CodeWriter : IDisposable
    {
        public int Indentation { get; set; }

        public TextWriter Writer { get; private set; }

        public CodeWriter(TextWriter writer, int initialIndentation = 0)
        {
            Writer = writer;

            Indentation = initialIndentation;
        }

        public void Dispose()
        {
            Flush();
        }

        public abstract void Flush();
    }
}
