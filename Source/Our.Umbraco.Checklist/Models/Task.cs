namespace Our.Umbraco.Checklist.Models
{
    public class Task
    {
        public int Id { get; set; }

        public int ContentNodeId { get; set; }

        public string Content { get; set; }

        public bool IsReady { get; set; }

        public bool IsDeleted { get; set; }
    }
}
