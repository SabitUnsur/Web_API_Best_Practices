using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory() 
        {
            return CreateActionResult(await _productService.GetProductWithCategory());
        }

        //GET api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>((products).ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }

        //[ValidateFilterAttribute]
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        //GET api/products/5 
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            var AddedproductDto = _mapper.Map<List<ProductDto>>(product);
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(201, AddedproductDto));
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
             await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<List<NoContentDto>>.Success(204));
        }

        //DELETE api/products/5 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             var product = await _productService.GetByIdAsync(id);
             await _productService.RemoveAsync(product);
             return CreateActionResult(CustomResponseDto<List<NoContentDto>>.Success(204));
        }

    }
}
