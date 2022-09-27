namespace PublicationSearch.Models
{
    public class CurrentPageViewModel
    {
        public int RecordsCount { get; set; }

        public List<PublicationViewModel> Publications { get; set; }
    }
}
