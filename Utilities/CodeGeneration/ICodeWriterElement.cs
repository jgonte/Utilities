using System.IO;

namespace CodeGeneration
{
    public interface ICodeWriterElement
    {
        CodeWriter CodeWriter { get; }

        void Indent(TextWriter writer);

        void Emit(TextWriter writer);
    }
}