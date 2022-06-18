using Application.DTO;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.RemoveProductByName
{
    /// <summary>
    /// CQRS pattern:RemoveProductByNameCommand command parameters
    /// </summary>
    public class RemoveProductByNameCommand : CommandBase, IRequest
    {
        /// <summary>
        /// Name of the product to delete
        /// </summary>
        public string ProductName { get; set; }
        

        public RemoveProductByNameCommand(string productName)
        {
            ProductName = productName;
        }
    }
}
