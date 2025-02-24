using System;

namespace OperatorPrecedenceParser
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
        public TokenType TokenType { get; set; }
        public string Value { get; set; }

        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public override string ToString()
        {
            return $"Token({TokenType}, {Value})";
        }
    }
}