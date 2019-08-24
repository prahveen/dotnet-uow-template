namespace Template.DTO.DTOLibrary
{
    public class SearchKeywordDTO : BaseDTO
    {
        public string Name { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string Action { get; set; }
    }
}
