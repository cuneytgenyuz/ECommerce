using ECommerce.DataAccess.Context;
using ECommerce.DataAccess.Repositories.Concrete;
using ECommerce.Entities.Entities.Concrete;

namespace ECommerce.DataAccess.Repositories.Concrete;
public class CategoryRepo : GenericRepo<Category>
{
    public CategoryRepo(ECommerceDbContext context) : base(context) { }

}