using System.IO;

namespace CodeGeneration
{
    public interface ICodeWriterElement
    {
        void Indent(TextWriter writer);

        void Emit(TextWriter writer);
    }
}