using AtulaTestWebsite.Data;
using AtulaTestWebsite.DataAccess.IRepository;
using AtulaTestWebsite.Models.Modles;

namespace AtulaTestWebsite.DataAccess.Repository
{
    internal class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}