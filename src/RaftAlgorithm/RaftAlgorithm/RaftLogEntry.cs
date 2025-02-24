namespace RaftAlgorithm
{
    public class RaftLogEntry
    {
        public int Index { get; set; }
        public int Term { get; set; }
        public string Command { get; set; }

        public RaftLogEntry(int index, int term, string command)
        {
            Index = index;
            Term = term;
            Command = command;
        }
    }
}