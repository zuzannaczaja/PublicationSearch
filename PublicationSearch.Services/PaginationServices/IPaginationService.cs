namespace PublicationSearch.Services.PaginationServices
{
    public interface IPaginationService
    {
        int GetPageSize(int newPage, int recordsOnPage);

        void SetCursorMark(string cursorMark);

        string GetCursorMark();

        void SetCurrentPage(int currentPage);

        int GetCurrentPage();
    }
}