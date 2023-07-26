using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
       /*private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }*/

        //API'den DTO dönüşlü geldiği için servis ve mapper injectionları kaldırdım.

        private readonly ProductApiService _productApiService;

        private readonly CategoryApiService _categoryApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }


        public async Task<IActionResult> Index()
        {
          /* var customResponse = await _productService.GetProductWithCategory();
            return View(customResponse.Data);*/

            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            /*var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();*/

            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                /*await _productService.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction("Index");*/
                await _productApiService.SaveAsync(productDto);
                return RedirectToAction("Index");
            }

            //İşlem başarısız olursa kategorinin tekrar yüklenebilmesi için yazıldı.

            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            //Seçim yapıldığı zaman bana Id'si dönecek, kulllanıcı Name görecek.
            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product= await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();

            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());


            ViewBag.categories = new SelectList(categoriesDto, "ID", "Name",product.CategoryId); //product.CategoryId --> selected value olarak verilir.
            //return View(_mapper.Map<ProductDto>(product));
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                //await _productService.UpdateAsync(_mapper.Map<Product>(productDto));

                await _productApiService.UpdateAsync(productDto);

                return RedirectToAction("Index");
            }

            /*  var categories = await _categoryService.GetAllAsync();
              var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
              ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);*/

            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "ID", "Name", productDto.CategoryId);

            return View(productDto);
        }


        public async Task<IActionResult> Remove(int id)
        {
            /*var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);*/

            //var product = await _productApiService.GetByIdAsync(id);

            await _productApiService.RemoveAsync(id); 
            return RedirectToAction("Index");
        }

    }
}
