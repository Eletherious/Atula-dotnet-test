namespace AtulaTestWebsite.DataAccess.IRepository
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IProductCategoryRepository ProductCategory { get; }
        void Save();
    }
}
