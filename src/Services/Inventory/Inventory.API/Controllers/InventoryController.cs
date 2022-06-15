﻿using Inventory.Application.Features.Products.Commands.AddProduct;
using Inventory.Application.Features.Products.Commands.RemoveProductByName;
using Inventory.Application.Features.Products.Queries.GetAllProducts;
using Inventory.Infrastructure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AuthorizeAttribute]
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
        /// Obtains all the products of the inventory. For simplicity, sorting is not implemented, only paging
        /// </summary>
        /// <param name="query">Search filter and paging parameters</param>
        /// <returns>The paginated list of all inventory products</returns>
        [HttpGet]
        [Route("GetProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts([FromQuery] GetAllProductsQuery query)
        {
            _logger.LogInformation("InventoryController - GetAllProducts");

            var orders = await _mediator.Send(query); //Mediator is responsible for sending each query/command to its corresponding handler
            return Ok(orders);
        }


        /// <summary>
        /// Adds a product to inventory. Name, Reference and Type are required. NumUnits must be greater than 0.
        /// </summary>
        /// <remarks>
        /// In order to simplify model, the following product data is modeled with an enum, indicating the possible values:
        /// 
        /// Possible product types: Primer = 0, PrimerKit = 1, ProbeKit = 2, BarcodeKit = 3
        /// 
        /// Possible manufacturers: Agilent = 0, Roche = 1
        /// 
        /// Possible suppliers: Supplier1 = 0, Supplier2 = 1, Supplier3 = 2
        /// 
        /// </remarks>
        /// <param name="command">Input parameter with product data</param>
        /// <returns>Information about the newly added product, such as its identifier or name</returns>
        [HttpPost]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(NewProductDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddProductToInventory([FromBody] AddProductCommand command)
        {
            _logger.LogInformation($"InventoryController - AddProductToInventory - {Newtonsoft.Json.JsonConvert.SerializeObject(command)}");

            var userName = HttpContext.Items["UserName"];
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// Remove a product from the inventory by name
        /// </summary>
        /// <param name="productName">Exact name of the product</param>
        /// <returns>No content (code 204) if it has been deleted correctly. Error (code 404) if the product does not exist</returns>
        [HttpDelete]
        [Route("RemoveProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder([Required] string productName)
        {
            var command = new RemoveProductByNameCommand(productName);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
