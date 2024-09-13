using AtulaTestWebsite.Data;
using AtulaTestWebsite.DataAccess.IRepository;
using AtulaTestWebsite.Models.Modles;
using AtulaTestWebsite.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AtulaTestWebsite.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
