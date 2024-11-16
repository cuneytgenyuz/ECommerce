using ECommerce.Entities.Entities.Concrete;

namespace ECommerce.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepo<Product> Products { get; }
    IGenericRepo<Category> Categories { get; }
    Task<int> CompleteAsync(); 
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
