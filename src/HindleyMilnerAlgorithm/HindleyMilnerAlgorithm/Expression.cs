namespace HindleyMilnerAlgorithm
{
    public abstract class Expr
    {
    }

    public class Var : Expr
    {
        public string Name { get; }

        public Var(string name)
        {
            Name = name;
        }
    }

    public class App : Expr
    {
        public Expr Function { get; }
        public Expr Argument { get; }

        public App(Expr function, Expr argument)
        {
            Function = function;
            Argument = argument;
        }
    }

    public class Lam : Expr
    {
        public string Param { get; }
        public Expr Body { get; }

        public Lam(string param, Expr body)
        {
            Param = param;
            Body = body;
        }
    }

    public class Let : Expr
    {
        public string VarName { get; }
        public Expr Definition { get; }
        public Expr Body { get; }

        public Let(string varName, Expr definition, Expr body)
        {
            VarName = varName;
            Definition = definition;
            Body = body;
        }
    }
}