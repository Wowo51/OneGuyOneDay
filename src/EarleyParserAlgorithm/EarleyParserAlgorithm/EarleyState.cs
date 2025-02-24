namespace EarleyParserAlgorithm
{
    public class EarleyState
    {
        public Rule Rule { get; set; }
        public int Dot { get; set; }
        public int Origin { get; set; }

        public EarleyState(Rule rule, int dot, int origin)
        {
            this.Rule = rule;
            this.Dot = dot;
            this.Origin = origin;
        }

        public bool IsComplete
        {
            get
            {
                return this.Dot >= this.Rule.RightHandSide.Length;
            }
        }

        public string? NextSymbol
        {
            get
            {
                return this.IsComplete ? null : this.Rule.RightHandSide[this.Dot];
            }
        }

        public override bool Equals(object? obj)
        {
            EarleyState? other = obj as EarleyState;
            if (other == null)
            {
                return false;
            }

            return object.Equals(this.Rule, other.Rule) && this.Dot == other.Dot && this.Origin == other.Origin;
        }

        public override int GetHashCode()
        {
            return (this.Rule != null ? this.Rule.GetHashCode() : 0) ^ this.Dot.GetHashCode() ^ this.Origin.GetHashCode();
        }
    }
}