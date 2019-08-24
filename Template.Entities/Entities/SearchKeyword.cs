namespace Template.Entities.Entities
{
    public class SearchKeyword :BaseEntity
    {
        public string Name { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string Action { get; set; }
    }
}
