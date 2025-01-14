using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PT_SalasDario.API.Infra;
using PT_SalasDario.API.Requests;
using PT_SalasDario.Services;
using PT_SalasDario.Services.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PT_SalasDario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductosController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<ActionResult> Get()
        {
            try
            {
                var products = await _productService.GetProductsWithStock();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult { StatusCode = 500, Errors = [ex.Message.ToString()] });
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var product = await _productService.GetProduct(id);

                if (product == null)
                {
                    var errorResult = new ErrorResult(400);
                    return BadRequest(errorResult);
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult(500) { Errors = [ex.Message.ToString()] });
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<ActionResult> Post([FromBody] CreateProductRequest request)
        {
            try
            {
                await _productService.CreateProduct(_mapper.Map<Services.Requests.CreateProductRequest>(request));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult(500) { Errors = [ex.Message.ToString()] });
            }
        }
    }
}
