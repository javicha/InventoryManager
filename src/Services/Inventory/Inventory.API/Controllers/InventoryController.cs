using Inventory.Application.Features.Products.Commands.AddProduct;
using Inventory.Application.Features.Products.Commands.RemoveProductByName;
using Inventory.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IMediator mediator, ILogger<InventoryController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Obtains all the products of the inventory. 
        /// For simplicity, paging and sorting is not implemented
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetInventoryProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            _logger.LogInformation("InventoryController - GetAllProducts");

            var query = new GetAllProductsQuery();
            var orders = await _mediator.Send(query); //Mediator is responsible for sending each query/command to its corresponding handler
            return Ok(orders);
        }


        /// <summary>
        /// Adds a product to inventory
        /// </summary>
        /// <param name="command">Input parameter with product data</param>
        /// <returns>Information about the newly added product, such as its identifier or name</returns>
        [HttpPost(Name = "AddProduct")]
        [ProducesResponseType(typeof(NewProductDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddProductToInventory([FromBody] AddProductCommand command)
        {
            _logger.LogInformation($"InventoryController - AddProductToInventory - {Newtonsoft.Json.JsonConvert.SerializeObject(command)}");

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// Remove a product from the inventoy by name
        /// </summary>
        /// <param name="productName">Exact name of the product</param>
        /// <returns>No content (code 204) if it has been deleted correctly. Error (code 404) if the product does not exist</returns>
        [HttpDelete("{productName}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder([Required] string productName)
        {
            var command = new RemoveProductByNameCommand() { ProductName = productName };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
