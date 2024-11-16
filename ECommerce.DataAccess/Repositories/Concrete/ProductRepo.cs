using ECommerce.DataAccess.Context;
using ECommerce.Entities.Entities.Concrete;

namespace ECommerce.DataAccess.Repositories.Concrete;

public class ProductRepo : GenericRepo<Product>
{
    public ProductRepo(ECommerceDbContext context) : base(context) { }

}