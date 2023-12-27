using Application.Interfaces.ECommerce;
using AutoMapper;
using Domain.Entities.ECommerce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using Presentation.DTOs.ECommerce;

namespace Presentation.Controllers.ECommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<Response<List<ProductListDTO>>> getProduct()
        {
            try
            {
                var product = await _productService.GetProducts();
                return new Response<List<ProductListDTO>>(_mapper.Map<List<ProductListDTO>>(product), true, "Successfully get Product list");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("{id}")]
        public async Task<Response<ProductResponseDTO>> GetProduct(Guid id)
        {

            var product = await _productService.GetProductDetail(id);
            return new Response<ProductResponseDTO>(_mapper.Map<ProductResponseDTO>(product));
        }

        [HttpPost]
        public async Task<Response<ProductRequestDTO>> SaveProduct(ProductRequestDTO product)
        {
            try
            {
                var savedROle = await _productService.SaveProduct(_mapper.Map<Product>(product));
                return new Response<ProductRequestDTO>(_mapper.Map<ProductRequestDTO>(savedROle), message: "Product Successfully Saved!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]

        public async Task<Response<ProductRequestDTO>> UpdateProduct(Guid Id, ProductRequestDTO product)
        {
            try
            {
                var Product = _mapper.Map<Product>(product);
                Product.Id = Id;
                var updatedProduct = await _productService.UpdateProduct(Id, Product);
                return new Response<ProductRequestDTO>(_mapper.Map<ProductRequestDTO>(updatedProduct), message: "Product Successfully Updated!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]

        public async Task<Response<ProductListDTO>> DeleteProduct(Guid id)
        {
            var deleteproduct = await _productService.DeleteProduct(id);
            return new Response<ProductListDTO>(_mapper.Map<ProductListDTO>(deleteproduct), true, "Product Successfully Deleted");
        }

    }
}
