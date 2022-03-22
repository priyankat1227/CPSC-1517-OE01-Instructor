using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;
using WestWindSystem.Entities;
using WebApp.Helpers;
#endregion

namespace ExampleWebApp.Pages.Samples
{
    public class PartialFilterSearchModel : PageModel
    {
        #region Private services fields and class constructor
        private readonly ILogger<PartialFilterSearchModel> _logger;
        private readonly TerritoryServices _territoryServices;

        public PartialFilterSearchModel(ILogger<PartialFilterSearchModel> logger, TerritoryServices territoryServices)
        {
            _logger = logger;
            _territoryServices = territoryServices;
        }
        #endregion

        [TempData]
        public string partialSearchFeedback { get; set; }

        [BindProperty(SupportsGet = true)]
        public string partialSearchText { get; set; }

        public List<Territory> TerritoryResults { get; set; } = new List<Territory>();

        #region Paginator
        //set the desired page size
        private const int PAGE_SIZE = 5;
        //be able to hold an instance of the Paginator Class
        public Paginator Pager { get; set; }
        #endregion

        public void OnGet(int? currentPage)
        {

            //using the Paginator with your query

            //OnGet ends up with a parameter (Request query string) that receives the current
            // page number. On the initial load of the page, this value will be null
            // to be null we had to add the ? after int.
            if(!string.IsNullOrEmpty(partialSearchText))
			{
                //temp value for the number of results for our query
                int totalCount;
                //set up for using the paginator if a query is run, no need if no query

                // determine the current page number

                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;
                //set up the current state of the paginator (with the sizing)
                PageState current = new(pageNumber, PAGE_SIZE);
                //do our query
                TerritoryResults = _territoryServices.GetByPartialDescription(partialSearchText, pageNumber, PAGE_SIZE, out totalCount);

                Pager = new Paginator(totalCount, current);
			}
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrEmpty(partialSearchText))
            {
                partialSearchFeedback = "Required: Search string is empty.";
            }

            return RedirectToPage(new { partialSearchText = partialSearchText });
        }

        public IActionResult OnPostClear()
        {
            partialSearchFeedback = "";
            ModelState.Clear();
            return RedirectToPage(new { PartialSearchText = (string?)null });
        }
    }
}
