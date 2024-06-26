using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Common;
using VerticalSliceArchitecture.Data;
using VerticalSliceArchitecture.Entities;

namespace VerticalSliceArchitecture.Features
{
    public static class GetProduct
    {
        public record Result(Guid Id, string Name, int Quantity);
        public record Query(Guid Id) : IRequest<Result>;
        internal sealed class QueryHandler : IRequestHandler<Query, Result>
        {
            public ApplicationDbContext _context;

            public QueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                Product? product = await _context.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
                if (product is null)
                    throw new Exception("Product is not found correspond to specified Id value");

                return new Result(product.Id, product.Name, product.Quantity);
            }
        }
    }
    internal sealed class QueryValidator : AbstractValidator<GetProduct.Query>
    {
        public QueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty().WithMessage("Product Id is reqired");
        }
    }

    [ApiExplorerSettings(GroupName = "Product")]
    public class GetProductEndpoint : ApiControllerBase
    {
        [HttpGet(BaseApiPath + "products/{id:guid}")]
        public async Task<IActionResult> CreateProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            GetProduct.Result product = await Mediator.Send(new GetProduct.Query(id), cancellationToken);

            return Ok(product);
        }
    }
}