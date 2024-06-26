using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Common;
using VerticalSliceArchitecture.Data;

namespace VerticalSliceArchitecture.Features
{
    public static class GetAllProducts
    {
        public record Result(Guid Id, string Name, int Quantity);
        public record Query : IRequest<List<Result>>;
        internal sealed class QueryHandler : IRequestHandler<Query, List<Result>>
        {
            public ApplicationDbContext _context;

            public QueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Result>> Handle(Query request, CancellationToken cancellationToken) => await _context.Products.Select(p => new GetAllProducts.Result(p.Id, p.Name, p.Quantity)).ToListAsync();
        }
    }

    [ApiExplorerSettings(GroupName = "Product")]
    public class GetAllProductsEndpoint : ApiControllerBase
    {
        [HttpGet(BaseApiPath + "products")]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var products = await Mediator.Send(new GetAllProducts.Query(), cancellationToken);

            return Ok(products);
        }
    }
}
