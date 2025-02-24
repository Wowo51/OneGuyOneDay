namespace EarleyParserAlgorithm
{
    public class Rule
    {
        public string LeftHandSide { get; set; }
        public string[] RightHandSide { get; set; }

        public Rule(string leftHandSide, string[] rightHandSide)
        {
            this.LeftHandSide = leftHandSide;
            this.RightHandSide = rightHandSide;
        }

        public override bool Equals(object? obj)
        {
            Rule? other = obj as Rule;
            if (other == null)
            {
                return false;
            }

            if (this.LeftHandSide != other.LeftHandSide)
            {
                return false;
            }

            if (this.RightHandSide.Length != other.RightHandSide.Length)
            {
                return false;
            }

            for (int i = 0; i < this.RightHandSide.Length; i++)
            {
                if (this.RightHandSide[i] != other.RightHandSide[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = this.LeftHandSide != null ? this.LeftHandSide.GetHashCode() : 0;
            foreach (string symbol in this.RightHandSide)
            {
                hash ^= (symbol != null ? symbol.GetHashCode() : 0);
            }

            return hash;
        }
    }
}