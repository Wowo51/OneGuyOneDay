namespace ListScheduling
{
    public class Job
    {
        public string Id { get; set; }
        public int ProcessingTime { get; set; }

        public Job(string id, int processingTime)
        {
            Id = id;
            ProcessingTime = processingTime;
        }
    }
}