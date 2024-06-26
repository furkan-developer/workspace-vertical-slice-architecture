using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceArchitecture.Common
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected const string BaseApiPath = "/api/";
        private IMediator _mediator;

        protected IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                    _mediator = HttpContext.RequestServices.GetRequiredService<IMediator>();

                return _mediator;
            }
        }
    }
}