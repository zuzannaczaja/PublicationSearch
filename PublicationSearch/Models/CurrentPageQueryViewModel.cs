namespace PublicationSearch.Models
{
    public class CurrentPageQueryViewModel
    {
        public string Query { get; set; }

        public int RecordCount { get; set; }

        public List<PublicationViewModel> Publications { get; set; }
    }
}
