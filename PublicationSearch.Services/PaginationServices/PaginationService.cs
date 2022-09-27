namespace PublicationSearch.Services.PaginationServices
{
    public class PaginationService : IPaginationService
    {
        private string _cursorMark = "*";
        private int _currentPage = 1;

        public int GetPageSize(int newPage, int recordsOnPage)
        {
            int pagesToLoad = _currentPage - newPage;

            _currentPage = newPage;

            if (pagesToLoad > 0)
            {
                _cursorMark = "*";
                return newPage * recordsOnPage;
            }
            else if (pagesToLoad == 0)
            {
                _cursorMark = "*";
                return recordsOnPage;
            }
            else
            {
                return pagesToLoad * -1 * recordsOnPage;
            }
        }

        public void SetCursorMark(string cursorMark)
        {
            _cursorMark = cursorMark;
        }

        public string GetCursorMark()
        {
            return _cursorMark;
        }

        public void SetCurrentPage(int currentPage)
        {
            _currentPage = currentPage;
        }

        public int GetCurrentPage()
        {
            return _currentPage;
        }
    }
}
