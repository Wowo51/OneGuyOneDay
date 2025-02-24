using System;
using System.Collections.Generic;
using System.Globalization;

namespace PrattParser
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _position;
        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _position = 0;
        }

        private Token CurrentToken()
        {
            if (_position < _tokens.Count)
            {
                return _tokens[_position];
            }

            return new Token(TokenType.EOF);
        }

        private Token ConsumeToken()
        {
            Token current = CurrentToken();
            _position++;
            return current;
        }

        private int GetPrecedence(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Plus:
                case TokenType.Minus:
                    return 10;
                case TokenType.Asterisk:
                case TokenType.Slash:
                    return 20;
                default:
                    return 0;
            }
        }

        public Expression ParseExpression(int rbp = 0)
        {
            Token token = ConsumeToken();
            Expression left = Nud(token);
            while (rbp < GetPrecedence(CurrentToken()))
            {
                token = ConsumeToken();
                left = Led(token, left);
            }

            return left;
        }

        private Expression Nud(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Number:
                    double numberValue;
                    if (!Double.TryParse(token.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out numberValue))
                    {
                        numberValue = 0;
                    }

                    return new NumberExpression(numberValue);
                case TokenType.Minus:
                    // Handle unary minus.
                    Expression right = ParseExpression(30);
                    return new UnaryExpression(token, right);
                case TokenType.LeftParen:
                    Expression expr = ParseExpression();
                    if (CurrentToken().Type == TokenType.RightParen)
                    {
                        ConsumeToken();
                    }

                    return expr;
                default:
                    // Fallback for unexpected tokens.
                    return new NumberExpression(0);
            }
        }

        private Expression Led(Token token, Expression left)
        {
            int precedence = GetPrecedence(token);
            Expression right = ParseExpression(precedence);
            return new BinaryExpression(left, token, right);
        }
    }
}