using Application.Abstractions.Messaging;
using EGC.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Update
{
    internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        public Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
