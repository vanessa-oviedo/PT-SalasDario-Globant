using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using PT_SalasDario.Services.Requests;
using PT_SalasDario.Services.Response;

namespace PT_SalasDario.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;

        }

        public async Task CreateProduct(CreateProductRequest request)
        {
            var category = _mapper.Map<Category>(request);
            await _categoryRepository.CreateCategory(category);

            var product = _mapper.Map<Product>(request);
            await _productRepository.CreateProduct(product);
        }

        public async Task<List<ProductListResponseDTO>> GetProductsWithStock()
        {
            var productListQuery = _productRepository.GetProducts().Where(w=> w.Stock > 0);

            var productList = await productListQuery.Select(
                c => new ProductListResponseDTO()
                {
                    Name = c.Name, 
                    Id = c.Id
                }).ToListAsync();

            return productList;
        }

        public async Task<ProductResponseDTO> GetProduct(int id)
        {
            var productEntity = await _productRepository.GetProduct(id);

            var product = _mapper.Map<ProductResponseDTO>(productEntity);
            return product;
        }
    }
}
