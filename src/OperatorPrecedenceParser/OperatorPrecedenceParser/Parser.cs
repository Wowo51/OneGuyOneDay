using System;

namespace OperatorPrecedenceParser
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;
        public Parser(string text)
        {
            _lexer = new Lexer(text);
            _currentToken = _lexer.GetNextToken();
        }

        private void Eat(TokenType tokenType)
        {
            if (_currentToken.TokenType == tokenType)
            {
                _currentToken = _lexer.GetNextToken();
            }
            else
            {
                _currentToken = new Token(TokenType.End, "");
            }
        }

        public ExpressionNode? Parse()
        {
            ExpressionNode? node = ParseExpression(0);
            return node;
        }

        private ExpressionNode? ParseExpression(int minPrecedence)
        {
            ExpressionNode? left = ParsePrimary();
            if (left == null)
            {
                return null;
            }

            while (IsOperator(_currentToken.TokenType) && GetPrecedence(_currentToken.TokenType) >= minPrecedence)
            {
                Token opToken = _currentToken;
                int precedence = GetPrecedence(opToken.TokenType);
                Eat(opToken.TokenType);
                ExpressionNode? right = ParseExpression(precedence + 1);
                if (right == null)
                {
                    return left;
                }

                left = new BinaryOperationNode(left, opToken, right);
            }

            return left;
        }

        private ExpressionNode? ParsePrimary()
        {
            Token token = _currentToken;
            if (token.TokenType == TokenType.Number)
            {
                Eat(TokenType.Number);
                double value = 0;
                if (double.TryParse(token.Value, out value))
                {
                    return new NumberNode(value);
                }

                return new NumberNode(0);
            }
            else if (token.TokenType == TokenType.LeftParen)
            {
                Eat(TokenType.LeftParen);
                ExpressionNode? node = ParseExpression(0);
                if (_currentToken.TokenType == TokenType.RightParen)
                {
                    Eat(TokenType.RightParen);
                }

                return node;
            }

            return null;
        }

        private bool IsOperator(TokenType tokenType)
        {
            return tokenType == TokenType.Plus || tokenType == TokenType.Minus || tokenType == TokenType.Multiply || tokenType == TokenType.Divide;
        }

        private int GetPrecedence(TokenType tokenType)
        {
            if (tokenType == TokenType.Plus || tokenType == TokenType.Minus)
            {
                return 1;
            }

            if (tokenType == TokenType.Multiply || tokenType == TokenType.Divide)
            {
                return 2;
            }

            return 0;
        }
    }
}