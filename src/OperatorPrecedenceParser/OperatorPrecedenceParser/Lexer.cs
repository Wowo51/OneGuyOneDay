using System;

namespace OperatorPrecedenceParser
{
    public class Lexer
    {
        private readonly string _text;
        private int _pos;
        private char _currentChar;
        public Lexer(string text)
        {
            _text = text;
            _pos = 0;
            _currentChar = _text.Length > 0 ? _text[0] : '\0';
        }

        private void Advance()
        {
            _pos++;
            if (_pos < _text.Length)
            {
                _currentChar = _text[_pos];
            }
            else
            {
                _currentChar = '\0';
            }
        }

        private void SkipWhitespace()
        {
            while (_currentChar != '\0' && char.IsWhiteSpace(_currentChar))
            {
                Advance();
            }
        }

        private string Number()
        {
            string result = "";
            while (_currentChar != '\0' && (char.IsDigit(_currentChar) || _currentChar == '.'))
            {
                result += _currentChar;
                Advance();
            }

            return result;
        }

        public Token GetNextToken()
        {
            while (_currentChar != '\0')
            {
                if (char.IsWhiteSpace(_currentChar))
                {
                    SkipWhitespace();
                    continue;
                }

                if (char.IsDigit(_currentChar) || _currentChar == '.')
                {
                    string numberValue = Number();
                    return new Token(TokenType.Number, numberValue);
                }

                if (_currentChar == '+')
                {
                    Advance();
                    return new Token(TokenType.Plus, "+");
                }

                if (_currentChar == '-')
                {
                    Advance();
                    return new Token(TokenType.Minus, "-");
                }

                if (_currentChar == '*')
                {
                    Advance();
                    return new Token(TokenType.Multiply, "*");
                }

                if (_currentChar == '/')
                {
                    Advance();
                    return new Token(TokenType.Divide, "/");
                }

                if (_currentChar == '(')
                {
                    Advance();
                    return new Token(TokenType.LeftParen, "(");
                }

                if (_currentChar == ')')
                {
                    Advance();
                    return new Token(TokenType.RightParen, ")");
                }

                Advance();
            }

            return new Token(TokenType.End, "");
        }
    }
}