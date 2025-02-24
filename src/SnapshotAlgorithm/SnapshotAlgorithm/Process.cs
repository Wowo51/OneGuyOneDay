namespace SnapshotAlgorithm
{
    public class Process : ISnapshotable
    {
        public string Id { get; private set; }

        private string _state;
        public Process(string id, string initialState)
        {
            this.Id = id;
            this._state = initialState;
        }

        public string GetState()
        {
            return this._state;
        }
    }
}