namespace SnapshotAlgorithm
{
    public interface ISnapshotable
    {
        string Id { get; }

        string GetState();
    }
}