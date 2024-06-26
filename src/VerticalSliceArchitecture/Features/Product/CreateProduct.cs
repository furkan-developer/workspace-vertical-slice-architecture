using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Common;
using VerticalSliceArchitecture.Data;
using VerticalSliceArchitecture.Entities;

namespace VerticalSliceArchitecture.Features
{
    public static class CreateProduct
    {
        public record Command(string Name, int Quantity) : IRequest<Guid>;
        internal sealed class CommandHandler : IRequestHandler<Command, Guid>
        {
            public ApplicationDbContext _context;

            public CommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(Command payload, CancellationToken cancellationToken)
            {
                var product = new Product()
                {
                    Name = payload.Name,
                    Quantity = payload.Quantity
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                return product.Id;
            }
        }
    }

    [ApiExplorerSettings(GroupName = "Product")]
    public class CreateProductEndpoint : ApiControllerBase
    {
        [HttpPost(BaseApiPath + "products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Command createCommand, CancellationToken cancellationToken)
        {
            Guid createdProductId = await Mediator.Send(createCommand, cancellationToken);

            return Ok(createdProductId);
        }
    }
}