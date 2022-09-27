using PublicationSearch.Services.PaginationServices;
using PublicationSearch.Services.PublicationServices.Models;
using WSCitationImplService;

namespace PublicationSearch.Services.PublicationServices
{
    public class GetPublicationService : IGetPublicationService
    {
        private readonly IPaginationService _paginationService;

        public GetPublicationService(IPaginationService paginationService)
        {
            _paginationService = paginationService;
        }

        public CurrentPageViewDto GetPublicationsAsync(string query, int newPage, int recordsOnPage)
        {
            var client = new WSCitationImplClient();

            var pageSize = _paginationService.GetPageSize(newPage, recordsOnPage);

            var cursorMark = _paginationService.GetCursorMark();

            if (newPage == 1)
            {
                cursorMark = "*";
            }

            var publicationTable = client.searchPublicationsAsync(query, string.Empty, cursorMark.ToString(), pageSize.ToString(),
                string.Empty, string.Empty, string.Empty).Result.@return;

            if (publicationTable.nextCursorMark != null)
            {
                _paginationService.SetCursorMark(publicationTable.nextCursorMark);

            }
            else
            {
                _paginationService.SetCursorMark("*");
            }

            List<PublicationViewDto> list = new();

            if (publicationTable.resultList.Length < recordsOnPage)
            {
                recordsOnPage = publicationTable.resultList.Length;
            }

            for (int i = publicationTable.resultList.Length - recordsOnPage; i < publicationTable.resultList.Length; i++)
            {
                list.Add(new PublicationViewDto
                {
                    Title = publicationTable.resultList[i].title,
                    Author = publicationTable.resultList[i].authorString,
                    JournalTitle = publicationTable.resultList[i].journalTitle,
                    FirstPublicationDate = publicationTable.resultList[i].firstPublicationDate
                });
            }

            return new CurrentPageViewDto
            {
                RecordsCount = publicationTable.hitCount,
                Publications = list
            };
        }
    }
}
