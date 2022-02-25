using System;
using System.Collections.Generic;
using System.Text;

namespace CAIS
{
    [Serializable]
    public class InvalidNMEAMessageException : Exception
    {
        public string AISMessage { get; }
        public InvalidNMEAMessageException() { }

        public InvalidNMEAMessageException(string message)
            : base(message) { }

        public InvalidNMEAMessageException(string message, Exception inner)
            : base(message, inner) { }
        public InvalidNMEAMessageException(string message, string AISMessage)
        : this(message)
        {
            this.AISMessage = AISMessage;
        }
    }


    [Serializable]
    public class UnknownMessageException : Exception
    {
        public string AISMessage { get; }
        public UnknownMessageException() { }

        public UnknownMessageException(string message)
            : base(message) { }

        public UnknownMessageException(string message, Exception inner)
            : base(message, inner) { }
        public UnknownMessageException(string message, string AISMessage)
        : this(message)
        {
            this.AISMessage = AISMessage;
        }
    }

    [Serializable]
    public class ValueException : Exception
    {
        public string AISMessage { get; }
        public char Char { get; }
        public ValueException() { }

        public ValueException(string message)
            : base(message) { }

        public ValueException(string message, Exception inner)
            : base(message, inner) { }
        public ValueException(string message, string AISMessage)
        : this(message)
        {
            this.AISMessage = AISMessage;
        }
        public ValueException(string message, char AISMessageChar)
        : this(message)
        {
            Char = AISMessageChar;
        }
        public ValueException(string message, string AISMessage, char AISMessageChar)
        : this(message)
        {
            this.AISMessage = AISMessage;
            Char = AISMessageChar;
        }
    }
}
