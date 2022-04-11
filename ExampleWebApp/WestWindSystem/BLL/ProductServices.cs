using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class ProductServices
    {
        #region Context Connection
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal ProductServices(WestWindContext regContext)
        {
            _context = regContext;
        }

        #endregion

        #region Queries
        public Product Product_GetByID(int value)
        {
            //Query and return the Product that matched the PK field value supplied in the value parameter
            //Linq by default is expected to return 0, 1 or many rows
            //WHEN your receive variable (product) expects only a SINGLE (1)
            //Instance of the class (Product), you must "tell" Linq to return the First instance or a default value

            return _context.Products
                    .Where(x => x.ProductID == value)
                    .FirstOrDefault();
              
        }
        #endregion

        #region Add, Update, Delete

        //Adding a record to your database, it may require you to verify
        // the data does not already exist on the database
        //We can do this by using a Linq query and a given set of verification fields to 
        // see if the product already exists.

        //Example: for product adding, we will verify there there is no product record in the database
        // with the same product name and quantity per unit from the same supplier. If there is
        // we will throw an exception.

        //you MUST know whether the table has an identity PK or not
        //if the table PK is NOT an identity then you MUST ensure
        // that the incoming data HAS a PK!
        //if the table PK is an identity record then the data WILL
        // generate the value for the PK for you, you do not supply
        // a PK in this case and the PK can be given back to you once
        // committed (the record is created)

        //Product PK is an identity field (attribute)
        //Make our method to optionally send the new identity value
        // back to the web page.

        public int Product_AddProduct(Product item)
        {
            //This section verifies if the data already exists (this is optional unless asked for)
            Product exists = _context.Products
                                .Where(x => x.ProductName.Equals(item.ProductName) &&
                                            x.QuantityPerUnit.Equals(item.QuantityPerUnit) &&
                                            x.SupplierID == item.SupplierID)
                                .FirstOrDefault();

            if(exists != null)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} from the selected supplier already exists.");
            }

            //stage the data in local memory to be submited to the database for storage

            //IMPORTANT: the data is in Local memory only! it has NOT yet been sent to the database!
            // THEREFORE: at this time, there is NO PK value (except a system default for ints of 0)
            _context.Products.Add(item);

            // commit the changed LOCAL data to the database
            _context.SaveChanges();

            //AFTER the commit, your PK value will not be able to be returned!
            return item.ProductID;
        }

        //Updates may also have design consideration for validation
        //Update may request that you check the record is still in the database.

        public int Product_UpdateProduct(Product item)
        {
            //For an update, you MUST have the actual PK value on your item (Product)
            //If you do not have the PK, this will fail!

            //check if exists with a returned instance (object/Product)
            //Product exists = _context.Products
            //                    .Where(x => x.ProductID == item.ProductID)
            //                    .FirstOrDefault();

            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID); 

            if(!exists)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} for the selected supplier is not on file.");
            }

            //stage the update in the change tracking
            EntityEntry<Product> updating = _context.Entry(item);

            //flag the entity as modified (updated)
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //During a modification (update), SaveChanges() returns the number of rows affected (changed)
            return _context.SaveChanges();
        }

        public int Product_DeleteProduct(Product item)
        {
            //for a delete, you MUST provide the PK value in the instance
            //if not, there delete will not proceed as expected.

            //check if exists with a returned instance (object/Product)
            //Product exists = _context.Products
            //                    .Where(x => x.ProductID == item.ProductID)
            //                    .FirstOrDefault();

            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID);

            if (!exists)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} for the selected supplier is not on file.");
            }

            //Removing a record from your database it may a
            // physical removal or a logical removal

            //Physical Removal steps
            // 1) stage the record with a EntityState of .Deleted
            // 2) _context.SaveChanges() to complete the deletion
            // NOTE: If the record is a parent record (has children through FK) the delete will fail
            //   This is a SQL function as child records will not be automatically deleted.

            //stage the phyiscal delete
            //EntityEntry<Product> deleting = _context.Entry(item);
            //flag the entity to be deleted
            //deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            //Logical Delete
            // Logical delete is used when you should not or can not (has child records) remove a record
            //   from the database.
            // A decision will be made to logically delete based on business practice or business rules
            //  Could be because of Government Regulation
            // Logical deletes will update an attibute (property) of the record
            // NOTE: Look for attribute such as Active, Discontinued, a special date (example: ReleaseDate)

            // Product the logical delete is Discontinued = true

            //stage the logical delete (make the change)
            item.Discontinued = true;
            EntityEntry<Product> updating = _context.Entry(item);
            //flag the entity as modified
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //during the commit, SaveChanges() return the number of rows updated (affected by the update)
            return _context.SaveChanges();

        }

        #endregion

        #region Queries
        public List<Product> Product_CategoryProducts(int searchCatagoryID)
        {
            return _context.Products
                    .Where(x => x.CategoryID == searchCatagoryID)
                    .OrderBy(x => x.ProductName).ToList();
        }
        #endregion
    }
}
