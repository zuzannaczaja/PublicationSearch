using AutoMapper;
using PublicationSearch.Models;
using PublicationSearch.Services.PublicationServices.Models;

namespace PublicationSearch
{
    public class PublicationSearchMappingStore : Profile
    {
        public PublicationSearchMappingStore()
        {
            CreateMap<PublicationViewDto, PublicationViewModel>();
            CreateMap<CurrentPageViewDto, CurrentPageViewModel>();
        }
    }
}
