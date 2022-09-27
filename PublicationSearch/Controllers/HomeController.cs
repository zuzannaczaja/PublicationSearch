using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PublicationSearch.Models;
using PublicationSearch.Services.PaginationServices;
using PublicationSearch.Services.PublicationServices;
using System.Diagnostics;

namespace PublicationSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGetPublicationService _getPublicationService;
        private readonly IPaginationService _paginationService;

        public HomeController(ILogger<HomeController> logger,
            IMapper mapper,
            IGetPublicationService getPublicationService,
            IPaginationService paginationService)
        {
            _logger = logger;
            _mapper = mapper;
            _getPublicationService = getPublicationService;
            _paginationService = paginationService;
        }

        public IActionResult Index()
        {
            return View(new List<PublicationViewModel>()); 
        }

        public IActionResult Search(string query, int recordsOnPage, int page = 1)
        {
            try
            {
                if (page != 1 && page - _paginationService.GetCurrentPage() > 40)
                {
                    page = _paginationService.GetCurrentPage() + 40;
                }

                ViewData["DecreasePageByFive"] = page - 5;
                ViewData["DecreasePageByTwo"] = page - 2;
                ViewData["DecreasePageByOne"] = page - 1;
                ViewData["CurrentPage"] = page;
                ViewData["IncreasePageByOne"] = page + 1;
                ViewData["IncreasePageByTwo"] = page + 2;
                ViewData["IncreasePageByFive"] = page + 5;

                if (query == null)
                {
                    query = "SRC:*";
                }

                var listWithPageViewModel = _mapper.Map<CurrentPageViewModel>(_getPublicationService.GetPublicationsAsync(query, page, recordsOnPage));

                if (listWithPageViewModel.Publications.Count == 0)
                {
                    return View("NotFound");
                }

                ViewData["TotalPages"] = (int)Math.Ceiling(listWithPageViewModel.RecordsCount / (double)recordsOnPage);

                return View(new CurrentPageQueryViewModel
                {
                    Query = query,
                    RecordCount = listWithPageViewModel.RecordsCount,
                    Publications = listWithPageViewModel.Publications
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Error message:" + e.Message);
                return BadRequest("Error message:" + e.Message);
            }   
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}