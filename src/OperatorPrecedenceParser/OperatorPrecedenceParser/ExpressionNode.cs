using System;

namespace OperatorPrecedenceParser
{
    public abstract class ExpressionNode
    {
        public abstract double Evaluate();
    }

    public class NumberNode : ExpressionNode
    {
        public double Value { get; }

        public NumberNode(double value)
        {
            Value = value;
        }

        public override double Evaluate()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class BinaryOperationNode : ExpressionNode
    {
        public ExpressionNode Left { get; }
        public ExpressionNode Right { get; }
        public Token OperatorToken { get; }

        public BinaryOperationNode(ExpressionNode left, Token operatorToken, ExpressionNode right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override double Evaluate()
        {
            double leftValue = Left.Evaluate();
            double rightValue = Right.Evaluate();
            if (OperatorToken.TokenType == TokenType.Plus)
            {
                return leftValue + rightValue;
            }
            else if (OperatorToken.TokenType == TokenType.Minus)
            {
                return leftValue - rightValue;
            }
            else if (OperatorToken.TokenType == TokenType.Multiply)
            {
                return leftValue * rightValue;
            }
            else if (OperatorToken.TokenType == TokenType.Divide)
            {
                if (rightValue != 0)
                {
                    return leftValue / rightValue;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        public override string ToString()
        {
            return $"({Left} {OperatorToken.Value} {Right})";
        }
    }
}