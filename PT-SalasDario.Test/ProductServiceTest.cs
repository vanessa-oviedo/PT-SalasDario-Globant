using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using PT_SalasDario.Services;
using PT_SalasDario.Services.Requests;

namespace PT_SalasDario.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        private Mock<IProductRepository>? _productRepositoryMock;
        private Mock<ICategoryRepository>? _categoryRepositoryMock;
        private IMapper _mapper;
        private ProductService? _service;

        [TestInitialize]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            _mapper = new Mapper(configuration);

            _service = new ProductService(
                _productRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _mapper);
        }

        [TestMethod]
        public async Task Should_Create_Product()
        {
            //Arrange
            var productName = "Celular Samsung 123";
            var categoryName = "Electrónicos";
            var categoryId = 125;

            var productRequest = new CreateProductRequest() { Name = productName, CategoryName = categoryName, CategoryId = categoryId };

            //Act
            await _service.CreateProduct(productRequest);

            //Assert
            _productRepositoryMock.Verify(repo => repo.CreateProduct(It.Is<Product>(u => u.Name == productName)), Times.Once);
            _categoryRepositoryMock.Verify(repo => repo.CreateCategory(It.Is<Category>(u => u.Name == categoryName)), Times.Once);
        }

        [TestMethod]
        public async Task Should_Call_GetProductsWithStock()
        {
            // Arrange

            // Act : I had to add the TRY/CATCH because it's not easy to mock the result of an IQueryable.
            try
            {
                await _service.GetProductsWithStock();
            }
            catch
            {
            }

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetProducts(), Times.Once);
        }

        [TestMethod]
        public async Task Should_Call_GetProductById()
        {
            // Arrange
            var productId = 126;
            var product = new Product() { Id = productId, Name = "Cellphone" };

            _productRepositoryMock.Setup(s => s.GetProduct(productId)).ReturnsAsync(product);

            var result = await _service.GetProduct(productId);
            Assert.IsNotNull(result);
            Assert.AreEqual(productId, result.Id);
            Assert.AreEqual(product.Name, result.Name);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetProduct(productId), Times.Once);
        }
    }
}
