namespace Our.Umbraco.Checklist.Models.Pocos
{
    using NPoco;

    using Our.Umbraco.Checklist.Constants;

    [TableName(TableConstants.Tasks.TableName)]    
    internal class Task
    {
        public int Id { get; set; }

        public int ContentNodeId { get; set; }

        public string Content { get; set; }

        public bool IsReady { get; set; }

        public bool IsDeleted { get; set; }
    }
}
