using System.Collections.Generic;
using System.Text;

namespace ShuntingYardAlgorithm
{
    public static class ShuntingYardConverter
    {
        public static string ConvertToPostfix(string infix)
        {
            if (infix == null)
            {
                return "";
            }

            List<string> output = new List<string>();
            Stack<char> operators = new Stack<char>();
            int index = 0;
            while (index < infix.Length)
            {
                char token = infix[index];
                if (char.IsWhiteSpace(token))
                {
                    index++;
                    continue;
                }

                if (char.IsDigit(token))
                {
                    StringBuilder number = new StringBuilder();
                    while (index < infix.Length && (char.IsDigit(infix[index]) || infix[index] == '.'))
                    {
                        number.Append(infix[index]);
                        index++;
                    }

                    output.Add(number.ToString());
                    continue;
                }

                if (IsOperator(token))
                {
                    while (operators.Count > 0 && IsOperator(operators.Peek()))
                    {
                        char top = operators.Peek();
                        if ((IsLeftAssociative(token) && GetPrecedence(token) <= GetPrecedence(top)) || (!IsLeftAssociative(token) && GetPrecedence(token) < GetPrecedence(top)))
                        {
                            output.Add(operators.Pop().ToString());
                        }
                        else
                        {
                            break;
                        }
                    }

                    operators.Push(token);
                    index++;
                    continue;
                }

                if (token == '(')
                {
                    operators.Push(token);
                    index++;
                    continue;
                }

                if (token == ')')
                {
                    while (operators.Count > 0)
                    {
                        char op = operators.Pop();
                        if (op == '(')
                        {
                            break;
                        }

                        output.Add(op.ToString());
                    }

                    index++;
                    continue;
                }

                if (char.IsLetter(token))
                {
                    StringBuilder variable = new StringBuilder();
                    while (index < infix.Length && char.IsLetterOrDigit(infix[index]))
                    {
                        variable.Append(infix[index]);
                        index++;
                    }

                    output.Add(variable.ToString());
                    continue;
                }

                // Skip any unknown token
                index++;
            }

            while (operators.Count > 0)
            {
                output.Add(operators.Pop().ToString());
            }

            return string.Join(" ", output);
        }

        private static bool IsOperator(char token)
        {
            return token == '+' || token == '-' || token == '*' || token == '/' || token == '^';
        }

        private static int GetPrecedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 2;
                case '*':
                case '/':
                    return 3;
                case '^':
                    return 4;
                default:
                    return 0;
            }
        }

        private static bool IsLeftAssociative(char op)
        {
            // Exponent is treated as right-associative
            return op != '^';
        }
    }
}