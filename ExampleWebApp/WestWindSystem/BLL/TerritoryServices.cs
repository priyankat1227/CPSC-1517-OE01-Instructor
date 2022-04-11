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
	public class TerritoryServices
	{
        #region Context Connection
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal TerritoryServices(WestWindContext regContext)
        {
            _context = regContext;
        }
        #endregion

        #region Queries

        //query by a string
        public List<Territory> GetByPartialDescription(string partialDescription, int pageNumber, int pageSize, out int totalCount)
		{
            int skipRows;
            IEnumerable<Territory> info = _context.Territories
                                    .Where(territory => territory.TerritoryDescription.Contains(partialDescription))
                                    .OrderBy(territory => territory.TerritoryDescription);
            //set the totalCount so it can be provided to the caller of the method.
            totalCount = info.Count();
            //determine the number of rows to skip (if I'm on page 4 I need to not
            //return the first 3 pages of records.
            //(index - 1) * pageSize
            //PageNumber needs to be treated as a index (natural number - 1)
            skipRows = (pageNumber - 1) * pageSize;
            //return only the required number of rows
            //this will be done using filters through Linq
            //use the filter .Skip(n) to skip over n rows from the beginning of the collection
            //use the filter .Take(n) to take only the next n rows from the collection
            return info.Skip(skipRows).Take(pageSize).ToList();
		}

        public List<Territory> GetByRegionID (int regionID, int pageNumber, int pageSize, out int totalCount)
		{
            int skipRows;
            IEnumerable<Territory> info = _context.Territories
                                    .Where(territory => territory.RegionID == regionID)
                                    .OrderBy(territory => territory.TerritoryDescription);
            totalCount = info.Count();
            skipRows = (pageNumber - 1) * pageSize;
            return info.Skip(skipRows).Take(pageSize).ToList();
        }

        public Territory GetTerritoryByID(string territoryID)
        {
            return _context.Territories
                    .Where(territory => territory.TerritoryID.Equals(territoryID))
                    .FirstOrDefault();
        }

        #endregion
    }
}
