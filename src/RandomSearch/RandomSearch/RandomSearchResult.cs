namespace RandomSearch
{
    public class RandomSearchResult
    {
        public double[] Candidate { get; set; }
        public double ObjectiveValue { get; set; }

        public RandomSearchResult(double[] candidate, double objectiveValue)
        {
            Candidate = candidate;
            ObjectiveValue = objectiveValue;
        }
    }
}