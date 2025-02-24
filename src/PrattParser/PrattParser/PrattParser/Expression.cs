using System;

namespace PrattParser
{
    public abstract class Expression
    {
    }

    public class NumberExpression : Expression
    {
        public double Value { get; }

        public NumberExpression(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class BinaryExpression : Expression
    {
        public Expression Left { get; }
        public Token Operator { get; }
        public Expression Right { get; }

        public BinaryExpression(Expression left, Token operatorToken, Expression right)
        {
            Left = left;
            Operator = operatorToken;
            Right = right;
        }

        public override string ToString()
        {
            return $"({Left} {Operator.Value} {Right})";
        }
    }

    public class UnaryExpression : Expression
    {
        public Token Operator { get; }
        public Expression Operand { get; }

        public UnaryExpression(Token operatorToken, Expression operand)
        {
            Operator = operatorToken;
            Operand = operand;
        }

        public override string ToString()
        {
            return $"({Operator.Value}{Operand})";
        }
    }
}