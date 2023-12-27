using Domain.Entities.ECommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ECommerce
{
    public interface IProductService
    {

        public Task<List<Product>> GetProducts();
        public Task<Product> GetProductDetail(Guid id);
        public Task<Product> SaveProduct(Product product);
        public Task<Product> UpdateProduct(Guid id, Product product);
        public Task<Product> DeleteProduct(Guid id);   
    }
}
