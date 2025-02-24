using System;
using System.Text;

namespace SimplePrecedenceParser
{
    public abstract class Expr
    {
    }

    public class NumberExpr : Expr
    {
        public double Value { get; }

        public NumberExpr(double value)
        {
            Value = value;
        }
    }

    public class BinaryExpr : Expr
    {
        public Expr Left { get; }
        public char Operator { get; }
        public Expr Right { get; }

        public BinaryExpr(Expr left, char op, Expr right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }
    }

    public class PrecedenceParser
    {
        private string text;
        private int position;
        private char currentChar;
        public Expr Parse(string input)
        {
            text = input ?? string.Empty;
            position = 0;
            currentChar = text.Length > 0 ? text[0] : '\0';
            return ParseExpression();
        }

        private void Advance()
        {
            position++;
            if (position < text.Length)
            {
                currentChar = text[position];
            }
            else
            {
                currentChar = '\0';
            }
        }

        private void SkipWhitespace()
        {
            while (currentChar == ' ' || currentChar == '\t')
            {
                Advance();
            }
        }

        private double ParseNumber()
        {
            StringBuilder sb = new StringBuilder();
            while ((currentChar >= '0' && currentChar <= '9') || currentChar == '.')
            {
                sb.Append(currentChar);
                Advance();
            }

            double result;
            if (Double.TryParse(sb.ToString(), out result))
            {
                return result;
            }

            return 0;
        }

        private Expr ParseExpression()
        {
            return ParseTerm();
        }

        private Expr ParseTerm()
        {
            Expr left = ParseFactor();
            SkipWhitespace();
            while (currentChar == '+' || currentChar == '-')
            {
                char op = currentChar;
                Advance();
                SkipWhitespace();
                Expr right = ParseFactor();
                left = new BinaryExpr(left, op, right);
                SkipWhitespace();
            }

            return left;
        }

        private Expr ParseFactor()
        {
            Expr left = ParsePrimary();
            SkipWhitespace();
            while (currentChar == '*' || currentChar == '/')
            {
                char op = currentChar;
                Advance();
                SkipWhitespace();
                Expr right = ParsePrimary();
                left = new BinaryExpr(left, op, right);
                SkipWhitespace();
            }

            return left;
        }

        private Expr ParsePrimary()
        {
            SkipWhitespace();
            if (currentChar == '(')
            {
                Advance();
                Expr expr = ParseExpression();
                SkipWhitespace();
                if (currentChar == ')')
                {
                    Advance();
                }

                return expr;
            }

            double number = ParseNumber();
            return new NumberExpr(number);
        }
    }
}