using ECommerce.Core.Interfaces;
using ECommerce.DataAccess.Context;
using ECommerce.Entities.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.DataAccess.Repositories.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceDbContext _context;
    private GenericRepo<Product> _productRepo;
    private GenericRepo<Category> _categoryRepo;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(ECommerceDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Repository'leri lazy yükleme
    public IGenericRepo<Product> Products => _productRepo ??= new GenericRepo<Product>(_context);
    public IGenericRepo<Category> Categories => _categoryRepo ??= new GenericRepo<Category>(_context);

    // Transaction işlemleri
    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("Zaten bir transaction başlatılmış durumda.");

        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("Başlatılmış bir transaction yok.");

        try
        {
            await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync(); // Hata durumunda rollback
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("Başlatılmış bir transaction yok.");

        await _currentTransaction.RollbackAsync();
        await DisposeTransactionAsync();
    }

    // SaveChanges işlemi
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // Transaction objesini temizle
    private async Task DisposeTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    // Dispose işlemi
    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }
}