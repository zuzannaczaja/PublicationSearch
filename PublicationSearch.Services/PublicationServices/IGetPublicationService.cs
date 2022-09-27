using PublicationSearch.Services.PublicationServices.Models;
using WSCitationImplService;

namespace PublicationSearch.Services.PublicationServices
{
    public interface IGetPublicationService
    {
        CurrentPageViewDto GetPublicationsAsync(string query, int page, int recordsOnPage);
    }
}