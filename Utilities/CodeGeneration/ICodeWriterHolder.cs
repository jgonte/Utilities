namespace CodeGeneration
{
    public interface ICodeWriterHolder<T> where T : CodeWriter
    {
        T Builder { get; set; }
    }
}