using PT_SalasDario.Services.Requests;
using PT_SalasDario.Services.Response;

namespace PT_SalasDario.Services
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductRequest createProductRequest);

        Task<List<ProductListResponseDTO>> GetProductsWithStock();
        
        Task<ProductResponseDTO> GetProduct(int id);
    }
}
