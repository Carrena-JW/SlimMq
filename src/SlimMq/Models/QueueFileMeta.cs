namespace SlimMq.Models
{
    internal class QueueFileMeta
    {
        public string From { get; set; } = string.Empty; // published target
        public string TypeName { get; set; } = string.Empty;
        public bool IsEncryted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
