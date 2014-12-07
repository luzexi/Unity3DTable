namespace IniSharp.Exceptions
{
    internal class ParsingException : Exception
    {
        public ParsingException()
        {
        }

        public ParsingException(string message)
            : base(message)
        {
        }
    }
}

