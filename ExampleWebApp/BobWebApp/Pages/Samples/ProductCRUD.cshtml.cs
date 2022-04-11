using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;
using WestWindSystem.Entities;
using WebApp.Helpers;
#endregion

namespace ExampleWebApp.Pages.Samples
{
    public class ProductCRUDModel : PageModel
    {
        #region Private services fields and class constructor
        private readonly ILogger<OrdersModel> _logger;
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;
        private readonly SupplierServices _supplierServices;

        public ProductCRUDModel(ILogger<OrdersModel> logger, CategoryServices categoryServices, ProductServices productServices, SupplierServices supplierServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
            _productServices = productServices;
            _supplierServices = supplierServices;
        }
        #endregion

        #region Feedback and Error Messages
        public string Feedback { get; set; }

        public bool HasFeedback => !string.IsNullOrWhiteSpace(Feedback);

        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        #endregion
        [BindProperty(SupportsGet = true)]
        public int? productID { get; set; }

        [BindProperty]
        public Product ProductInfo { get; set; }

        [BindProperty]
        public List<Category> CategoryList { get; set; }

        [BindProperty]
        public List<Supplier> SupplierList { get; set; }


        public void OnGet()
        {
            PopulateLists();
            if(productID.HasValue && productID > 0)
            {
                ProductInfo = _productServices.Product_GetByID(productID.Value);
            }
        }
        private void PopulateLists()
        {
            CategoryList = _categoryServices.Category_List();
            SupplierList = _supplierServices.Supplier_List();
        }

        public IActionResult OnPostClear()
        {
            Feedback = "";
            ModelState.Clear();
            return RedirectToPage(new { productid = (int?)null });
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/Samples/CategoryProducts");
        }

        public IActionResult OnPostNew()
        {
            if(ModelState.IsValid)
            {
                //We will always do new with user friendly error handling
                try
                {
                    //call the appropriate service to try and add the new record
                    int productNew = _productServices.Product_AddProduct(ProductInfo);
                    Feedback = "Product was added.";
                    return RedirectToPage(new { productID = productNew });
                }
                catch (Exception ex)
                {
                    Feedback = GetInnerException(ex);
                    PopulateLists();
                    return Page();
                }
            }
            else
            {
                PopulateLists();
                return Page();
            }
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                //We will always do new with user friendly error handling
                try
                {
                    //call the appropriate service to try and update the record
                    int updated = _productServices.Product_UpdateProduct(ProductInfo);
                    Feedback = $"{updated} product(s) were updated.";
                    return RedirectToPage(new { productID = ProductInfo.ProductID });
                }
                catch (Exception ex)
                {
                    Feedback = GetInnerException(ex);
                    PopulateLists();
                    return Page();
                }
            }
            else
            {
                PopulateLists();
                return Page();
            }
        }

        public IActionResult OnPostDelete()
        {
            if (ModelState.IsValid)
            {
                //We will always do new with user friendly error handling
                try
                {
                    //call the appropriate service to try and update the record
                    int deleted = _productServices.Product_DeleteProduct(ProductInfo);
                    Feedback = $"{deleted} product(s) were discontinued.";
                    return RedirectToPage(new { productID = ProductInfo.ProductID });
                }
                catch (Exception ex)
                {
                    Feedback = GetInnerException(ex);
                    PopulateLists();
                    return Page();
                }
            }
            else
            {
                PopulateLists();
                return Page();
            }
        }

        private string GetInnerException(Exception ex)
        {
            while(ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex.Message;
        }
    }
}
