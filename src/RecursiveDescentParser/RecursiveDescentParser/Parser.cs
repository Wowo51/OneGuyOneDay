using System;
using System.Collections.Generic;
using System.Globalization;

namespace RecursiveDescentParser
{
    public class Parser
    {
        private List<Token> tokens;
        private int currentIndex;
        public string? Error { get; private set; }

        public Parser()
        {
            this.tokens = new List<Token>();
            this.currentIndex = 0;
            this.Error = null;
        }

        public double Parse(string input)
        {
            Lexer lexer = new Lexer();
            this.tokens = lexer.Tokenize(input);
            this.currentIndex = 0;
            double result = ParseExpression();
            if (CurrentToken().Type != TokenType.End)
            {
                this.Error = "Unexpected token at end of expression.";
                return 0;
            }

            return result;
        }

        private Token CurrentToken()
        {
            if (currentIndex < tokens.Count)
            {
                return tokens[currentIndex];
            }

            return new Token(TokenType.End, "");
        }

        private void NextToken()
        {
            if (currentIndex < tokens.Count)
            {
                currentIndex++;
            }
        }

        private double ParseExpression()
        {
            double result = ParseTerm();
            while (CurrentToken().Type == TokenType.Plus || CurrentToken().Type == TokenType.Minus)
            {
                Token operatorToken = CurrentToken();
                NextToken();
                double term = ParseTerm();
                if (operatorToken.Type == TokenType.Plus)
                {
                    result = result + term;
                }
                else
                {
                    result = result - term;
                }
            }

            return result;
        }

        private double ParseTerm()
        {
            double result = ParseFactor();
            while (CurrentToken().Type == TokenType.Multiply || CurrentToken().Type == TokenType.Divide)
            {
                Token operatorToken = CurrentToken();
                NextToken();
                double factor = ParseFactor();
                if (operatorToken.Type == TokenType.Multiply)
                {
                    result = result * factor;
                }
                else
                {
                    if (factor == 0)
                    {
                        this.Error = "Division by zero.";
                        return 0;
                    }

                    result = result / factor;
                }
            }

            return result;
        }

        private double ParseFactor()
        {
            Token token = CurrentToken();
            if (token.Type == TokenType.Number)
            {
                NextToken();
                double value;
                if (double.TryParse(token.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value))
                {
                    return value;
                }
                else
                {
                    this.Error = "Invalid number format.";
                    return 0;
                }
            }
            else if (token.Type == TokenType.LeftParen)
            {
                NextToken();
                double value = ParseExpression();
                if (CurrentToken().Type == TokenType.RightParen)
                {
                    NextToken();
                    return value;
                }

                this.Error = "Missing closing parenthesis.";
                return 0;
            }
            else
            {
                this.Error = "Unexpected token.";
                return 0;
            }
        }
    }
}