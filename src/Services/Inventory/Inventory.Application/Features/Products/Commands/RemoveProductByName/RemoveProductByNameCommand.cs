using MediatR;

namespace Inventory.Application.Features.Products.Commands.RemoveProductByName
{
    /// <summary>
    /// CQRS pattern:RemoveProductByNameCommand command parameters
    /// </summary>
    public class RemoveProductByNameCommand : IRequest
    {
        public string ProductName { get; set; }

        public RemoveProductByNameCommand(string productName)
        {
            ProductName = productName;
        }
    }
}
