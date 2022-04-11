using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
using WestWindSystem.Models;
#endregion

namespace WestWindSystem.BLL
{
    public class OrderServices
    {
        #region Context Connection
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal OrderServices(WestWindContext regContext)
        {
            _context = regContext;
        }
        #endregion

        #region Queries
        public List<Order> GetAllOrder(int pageNumber, int pageSize, out int totalCount)
        {
            int skipRows;
            IEnumerable<Order> info = _context.Orders;
            totalCount = info.Count();
            skipRows = (pageNumber - 1) * pageSize;
            return info.Skip(skipRows).Take(pageSize).ToList();
        }
        public List<Order> GetOrderByCustomerID(string customerID, int pageNumber, int pageSize, out int totalCount)
        {
            int skipRows;
            IEnumerable<Order> info = _context.Orders.Where(order => order.CustomerID.Equals(customerID));
            totalCount = info.Count();
            skipRows = (pageNumber - 1) * pageSize;
            return info.Skip(skipRows).Take(pageSize).ToList();
        }
        #endregion
    }
}
