using AtulaTestWebsite.Data;
using AtulaTestWebsite.DataAccess.IRepository;

namespace AtulaTestWebsite.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
            Category = new CategoryRepository(_db);
            ProductCategory = new ProductCategoryRepository(_db);
        }

        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IProductCategoryRepository ProductCategory { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
