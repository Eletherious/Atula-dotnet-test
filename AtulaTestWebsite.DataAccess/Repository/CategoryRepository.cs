using AtulaTestWebsite.Data;
using AtulaTestWebsite.DataAccess.IRepository;
using AtulaTestWebsite.Models.Modles;

namespace AtulaTestWebsite.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
