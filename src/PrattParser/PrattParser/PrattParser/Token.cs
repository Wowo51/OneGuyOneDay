using System;

namespace PrattParser
{
    public enum TokenType
    {
        Number,
        Plus,
        Minus,
        Asterisk,
        Slash,
        LeftParen,
        RightParen,
        EOF
    }

    public class Token
    {
        public TokenType Type { get; }
        public string? Value { get; }

        public Token(TokenType type, string? value = null)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"Token({Type}, {Value})";
        }
    }
}