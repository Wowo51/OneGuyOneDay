using System;
using System.Collections.Generic;

namespace LLParser
{
    public class ParseNode
    {
        public string NodeType { get; set; }
        public string? Value { get; set; }
        public List<ParseNode> Children { get; set; }

        public ParseNode(string nodeType, string? value)
        {
            NodeType = nodeType;
            Value = value;
            Children = new List<ParseNode>();
        }

        public ParseNode(string nodeType) : this(nodeType, null)
        {
        }
    }

    public class Parser
    {
        private string Input;
        private int Position;
        public Parser(string? input)
        {
            if (input == null)
            {
                input = "";
            }

            Input = input;
            Position = 0;
        }

        public ParseNode Parse()
        {
            ParseNode expression = ParseExpression();
            SkipWhiteSpace();
            if (Position < Input.Length)
            {
                ParseNode errorNode = new ParseNode("Error", "Unexpected input");
                expression.Children.Add(errorNode);
            }

            return expression;
        }

        private ParseNode ParseExpression()
        {
            ParseNode term = ParseTerm();
            SkipWhiteSpace();
            while (Position < Input.Length && Input[Position] == '+')
            {
                ParseNode plusNode = new ParseNode("Plus", "+");
                Position++;
                SkipWhiteSpace();
                ParseNode nextTerm = ParseTerm();
                ParseNode exprNode = new ParseNode("Expression");
                exprNode.Children.Add(term);
                exprNode.Children.Add(plusNode);
                exprNode.Children.Add(nextTerm);
                term = exprNode;
                SkipWhiteSpace();
            }

            return term;
        }

        private ParseNode ParseTerm()
        {
            return ParseNumber();
        }

        private ParseNode ParseNumber()
        {
            SkipWhiteSpace();
            int start = Position;
            while (Position < Input.Length && Char.IsDigit(Input[Position]))
            {
                Position++;
            }

            if (start == Position)
            {
                return new ParseNode("Error", "Number expected");
            }

            string number = Input.Substring(start, Position - start);
            return new ParseNode("Number", number);
        }

        private void SkipWhiteSpace()
        {
            while (Position < Input.Length && Char.IsWhiteSpace(Input[Position]))
            {
                Position++;
            }
        }
    }
}