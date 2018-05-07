using System.IO;

namespace CodeGeneration
{
    public abstract class CodeWriterElement<T> : ICodeWriterElement,
        ICodeWriterHolder<T>
        where T : CodeWriter
    {
        public CodeWriterElement(T codeWriter)
        {
            CodeWriter = codeWriter;
        }

        public T CodeWriter { get; set; }

        public abstract void Emit(TextWriter writer);

        public virtual void Indent(TextWriter writer)
        {
            writer.Write(new string(' ', CodeWriter.Indentation * 4));
        }

        public override string ToString()
        {
            using (var writer = new StringWriter())
            {
                Emit(writer);

                return writer.ToString();
            }
        }
    }
}