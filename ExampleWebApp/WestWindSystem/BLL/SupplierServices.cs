using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class SupplierServices
    {
        #region Context Connection
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal SupplierServices(WestWindContext regContext)
        {
            _context = regContext;
        }
        #endregion

        #region Queries
        public List<Supplier> Supplier_List()
        {
            return _context.Suppliers
                    .OrderBy(x => x.CompanyName).ToList();
        }
        #endregion
    }
}
