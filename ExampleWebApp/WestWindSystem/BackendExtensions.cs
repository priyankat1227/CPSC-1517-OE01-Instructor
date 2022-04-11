#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WestWindSystem.BLL;
using WestWindSystem.DAL;
#endregion



namespace WestWindSystem
{
    public static class BackendExtensions
    {
        public static void WWBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            //within this method, we will register all the services
            //that will be used by our system (context setup)
            //and will be provided by the system

            //setup the context service
            services.AddDbContext<WestWindContext>(options);

            //register the services classes

            //add any business logic layer class to the service collection
            //allow the web app to access the methods (services) within the BLL class

            //the argument for the AddTransient is called a factory
            //adding a localize method to get access to the services
            services.AddTransient<BuildVersionServices>((serviceProvider) =>
            {
                // get the dbcontext class that has been registered
                var context = serviceProvider.GetService<WestWindContext>();

                //create an instance of the service class (BuildVersionServices/BLL)
                //supply the context reference to this service class and return it.
                return new BuildVersionServices(context);
            });

            services.AddTransient<RegionServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new RegionServices(context);
            });

            services.AddTransient<TerritoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new TerritoryServices(context);
            });

            services.AddTransient<OrderServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new OrderServices(context);
            });

            services.AddTransient<CustomerServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new CustomerServices(context);
            });

            services.AddTransient<ProductServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new ProductServices(context);
            });

            services.AddTransient<SupplierServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new SupplierServices(context);
            });

            services.AddTransient<CategoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new CategoryServices(context);
            });
        }
    }
}
