using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;
using WestWindSystem.Entities;
using WebApp.Helpers;
#endregion

namespace ExampleWebApp.Pages.Samples
{
    public class ReceivingPageModel : PageModel
    {
        #region Private services fields and class constructor
        private readonly ILogger<ReceivingPageModel> _logger;
        private readonly TerritoryServices _territoryServices;
        private readonly RegionServices _regionServices;

        public ReceivingPageModel(ILogger<ReceivingPageModel> logger, TerritoryServices territoryServices, RegionServices regionServices)
        {
            _logger = logger;
            _territoryServices = territoryServices;
            _regionServices = regionServices;
        }
        #endregion

        [BindProperty(SupportsGet = true)]
        public string? territoryID { get; set; }

        public string territoryDescription { get; set; }

        public int regionID { get; set; }

        public List<Region> regionsList { get; set; }
        public void OnGet()
        {
            if(!string.IsNullOrEmpty(territoryID))
            {
                Territory territory = _territoryServices.GetTerritoryByID(territoryID);
                territoryDescription = territory.TerritoryDescription;
                regionID = territory.RegionID;
                regionsList = _regionServices.Region_List();
            }
        }
    }
}
