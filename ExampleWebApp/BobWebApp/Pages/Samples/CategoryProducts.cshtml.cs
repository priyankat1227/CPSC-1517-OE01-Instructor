using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;
using WestWindSystem.Entities;
using WebApp.Helpers;
#endregion

namespace ExampleWebApp.Pages.Samples
{
    public class CategoryProductsModel : PageModel
    {
        #region Private services fields and class constructor
        private readonly ILogger<OrdersModel> _logger;
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;
        private readonly SupplierServices _supplierServices;

        public CategoryProductsModel(ILogger<OrdersModel> logger, CategoryServices categoryServices, ProductServices productServices, SupplierServices supplierServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
            _productServices = productServices;
            _supplierServices = supplierServices;
        }
        #endregion

        [TempData]
        public string Feedback { get; set; }

        [BindProperty(SupportsGet =true)]
        public int searchCatagoryID { get; set; }

        public List<Product> ProductInfo { get; set; }

        [BindProperty]
        public List<Category> CategoryList { get; set; } = new();

        [BindProperty]
        public List<Supplier> SupplierList { get; set; } = new();
        public void OnGet()
        {
            PopulateLists();
            if(searchCatagoryID != 0)
            {
                ProductInfo = _productServices.Product_CategoryProducts(searchCatagoryID);
            }
        }

        private void PopulateLists()
        {
            CategoryList = _categoryServices.Category_List();
            SupplierList = _supplierServices.Supplier_List();
        }

        public IActionResult OnPostFetch()
        {
            if(searchCatagoryID == 0)
            {
                Feedback = "Required: Search category not selected.";
            }

            return RedirectToPage(new { searchCatagoryID = searchCatagoryID });
        }

        public IActionResult OnPostClear()
        {
            Feedback = "";
            ModelState.Clear();
            return RedirectToPage(new { searchCatagoryID = (int?)null });
        }

        public IActionResult OnPostNew()
        {
            return RedirectToPage("/Samples/ProductCRUD");
        }
    }
}
