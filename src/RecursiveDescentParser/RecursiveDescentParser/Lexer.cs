using System;
using System.Collections.Generic;
using System.Text;

namespace RecursiveDescentParser
{
    public class Lexer
    {
        public List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            int index = 0;
            while (index < input.Length)
            {
                char current = input[index];
                if (char.IsWhiteSpace(current))
                {
                    index++;
                }
                else if (char.IsDigit(current))
                {
                    StringBuilder numberBuilder = new StringBuilder();
                    while (index < input.Length && (char.IsDigit(input[index]) || input[index] == '.'))
                    {
                        numberBuilder.Append(input[index]);
                        index++;
                    }

                    tokens.Add(new Token(TokenType.Number, numberBuilder.ToString()));
                }
                else if (current == '+')
                {
                    tokens.Add(new Token(TokenType.Plus, current.ToString()));
                    index++;
                }
                else if (current == '-')
                {
                    tokens.Add(new Token(TokenType.Minus, current.ToString()));
                    index++;
                }
                else if (current == '*')
                {
                    tokens.Add(new Token(TokenType.Multiply, current.ToString()));
                    index++;
                }
                else if (current == '/')
                {
                    tokens.Add(new Token(TokenType.Divide, current.ToString()));
                    index++;
                }
                else if (current == '(')
                {
                    tokens.Add(new Token(TokenType.LeftParen, current.ToString()));
                    index++;
                }
                else if (current == ')')
                {
                    tokens.Add(new Token(TokenType.RightParen, current.ToString()));
                    index++;
                }
                else
                {
                    index++;
                }
            }

            tokens.Add(new Token(TokenType.End, ""));
            return tokens;
        }
    }
}