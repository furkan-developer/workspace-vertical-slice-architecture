using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
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
            public IMapper _mapper;

            public CommandHandler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
            {
                var product = _mapper.Map<Product>(command);

                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                return product.Id;
            }
        }
    }


    public class CommandValidator : AbstractValidator<CreateProduct.Command>
    {
        public CommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Product name is required");
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