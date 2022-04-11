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
    public class CategoryServices
    {
        #region Context Connection
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal CategoryServices(WestWindContext regContext)
        {
            _context = regContext;
        }


        #endregion

        #region Queries
        public List<Category> Category_List()
        {
            return _context.Categories
                    .OrderBy(x => x.CategoryName).ToList();
        }

        #endregion
    }
}
