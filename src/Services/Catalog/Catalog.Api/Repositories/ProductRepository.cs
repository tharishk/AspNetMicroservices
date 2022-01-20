using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ICatalogContext _CatalogContext { get; }
        public ProductRepository(ICatalogContext catalogContext)
        {
            _CatalogContext = catalogContext;
        }

        

        public async Task CreateProduct(Product product)
        {
             await _CatalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, Id);

            DeleteResult deleteResult = await _CatalogContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string Id)
        {
            return await _CatalogContext.Products.Find(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _CatalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string Name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, Name);

            return await _CatalogContext.Products.Find(filter).ToListAsync();

            
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _CatalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _CatalogContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id,
                                                                                                replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
