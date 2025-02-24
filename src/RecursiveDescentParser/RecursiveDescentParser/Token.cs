using System;

namespace RecursiveDescentParser
{
    public enum TokenType
    {
        Number,
        Plus,
        Minus,
        Multiply,
        Divide,
        LeftParen,
        RightParen,
        End
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}