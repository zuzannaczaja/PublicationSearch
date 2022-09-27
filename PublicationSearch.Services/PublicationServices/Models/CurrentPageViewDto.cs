namespace PublicationSearch.Services.PublicationServices.Models
{
    public class CurrentPageViewDto
    {
        public int RecordsCount { get; set; }

        public List<PublicationViewDto> Publications { get; set; }
    }
}
