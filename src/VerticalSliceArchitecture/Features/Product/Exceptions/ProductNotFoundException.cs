using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalSliceArchitecture.Common.Exceptions;

namespace VerticalSliceArchitecture.Features.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string message)
                   : base(message)
        {
        }
    }
}