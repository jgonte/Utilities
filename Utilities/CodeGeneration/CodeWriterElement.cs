using System.IO;

namespace CodeGeneration
{
    public abstract class CodeWriterElement<T> : ICodeWriterElement,
        ICodeWriterHolder<T>
        where T : CodeWriter
    {
        public CodeWriterElement(T codeWriter)
        {
            Builder = codeWriter;
        }

        public T Builder { get; set; }

        public abstract void Emit(TextWriter writer);

        public virtual void Indent(TextWriter writer)
        {
            writer.Write(new string(' ', Builder.Indentation * 4));
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