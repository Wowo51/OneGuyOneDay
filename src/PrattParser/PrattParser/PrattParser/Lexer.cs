using System;
using System.Collections.Generic;
using System.Globalization;

namespace PrattParser
{
    public class Lexer
    {
        private readonly string _text;
        private int _position;
        public Lexer(string text)
        {
            _text = text;
            _position = 0;
        }

        private char CurrentChar()
        {
            if (_position >= _text.Length)
            {
                return '\0';
            }

            return _text[_position];
        }

        private void Advance()
        {
            _position++;
        }

        private void SkipWhitespace()
        {
            while (CurrentChar() != '\0' && char.IsWhiteSpace(CurrentChar()))
            {
                Advance();
            }
        }

        private Token Number()
        {
            int start = _position;
            bool seenDot = false;
            while (CurrentChar() != '\0' && (char.IsDigit(CurrentChar()) || (!seenDot && CurrentChar() == '.')))
            {
                if (CurrentChar() == '.')
                {
                    seenDot = true;
                }

                Advance();
            }

            string numberStr = _text.Substring(start, _position - start);
            return new Token(TokenType.Number, numberStr);
        }

        public Token GetNextToken()
        {
            while (CurrentChar() != '\0')
            {
                if (char.IsWhiteSpace(CurrentChar()))
                {
                    SkipWhitespace();
                    continue;
                }

                if (char.IsDigit(CurrentChar()) || CurrentChar() == '.')
                {
                    return Number();
                }

                if (CurrentChar() == '+')
                {
                    Advance();
                    return new Token(TokenType.Plus, "+");
                }

                if (CurrentChar() == '-')
                {
                    Advance();
                    return new Token(TokenType.Minus, "-");
                }

                if (CurrentChar() == '*')
                {
                    Advance();
                    return new Token(TokenType.Asterisk, "*");
                }

                if (CurrentChar() == '/')
                {
                    Advance();
                    return new Token(TokenType.Slash, "/");
                }

                if (CurrentChar() == '(')
                {
                    Advance();
                    return new Token(TokenType.LeftParen, "(");
                }

                if (CurrentChar() == ')')
                {
                    Advance();
                    return new Token(TokenType.RightParen, ")");
                }

                // For any unrecognized character, simply advance.
                Advance();
            }

            return new Token(TokenType.EOF);
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            Token token = GetNextToken();
            while (token.Type != TokenType.EOF)
            {
                tokens.Add(token);
                token = GetNextToken();
            }

            tokens.Add(token); // Add EOF token.
            return tokens;
        }
    }
}