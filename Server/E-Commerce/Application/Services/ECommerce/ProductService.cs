using Application.Interfaces;
using Application.Interfaces.ECommerce;
using Domain.Entities.ECommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ECommerce
{
    public class ProductService : IProductService
    {


        private readonly IUnitOfWork _uow;
        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;

        }

        public async Task<List<Product>> GetProducts()
        {
            var product = _uow.GenericRepository<Product>().GetAll().ToList();
            return product;

        }


        public async Task<Product> GetProductDetail(Guid Id)
        {
            try
            {
                var product = await _uow.GenericRepository<Product>().GetFirstOrDefault(predicate: x => x.Id == Id, null,
                null,
              selector: x => x);

                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Product> SaveProduct(Product product)
        {
            try
            {
                var Saveproduct = await _uow.GenericRepository<Product>().InsertAsync(product);

                await _uow.SaveChangesAsync();
                return Saveproduct;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            try
            {
                var repo = _uow.GenericRepository<Product>();
                repo.Update(product);
                await _uow.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Delete
        public async Task<Product> DeleteProduct(Guid id)
        {
            try
            {
                var repo = _uow.GenericRepository<Product>();
                var product = repo.GetById(id);
                _uow.GenericRepository<Product>()
                    .Delete(product);
                await _uow.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

    }
}
