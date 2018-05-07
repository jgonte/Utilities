namespace CodeGeneration
{
    public interface ICodeWriterHolder<T> where T : CodeWriter
    {
        T CodeWriter { get; set; }
    }
}