namespace Utilities
{
    public class Tokenizer
    {
        protected char[] _buffer;

        protected int _offset = -1;

        /// <summary>
        /// The last character read from the buffer
        /// </summary>
        protected char _char;

        public Tokenizer(char[] buffer)
        {
            _buffer = buffer;

            ReadChar();
        }

        protected string GetValue(int end)
        {
            return new string(_buffer, _offset, end - _offset);
        }

        public bool End() => _offset >= _buffer.Length;

        protected void SkipSpaces()
        {
            while (char.IsWhiteSpace(_char))
            {
                ReadChar();
            }
        }

        protected void ReadChar()
        {
            ++_offset;

            if (_offset < _buffer.Length)
            {
                _char = _buffer[_offset];
            }
        }
    }
}
