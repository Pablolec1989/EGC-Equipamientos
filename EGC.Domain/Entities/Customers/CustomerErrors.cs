using EGC.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Entities.Clients
{
    public class CustomerErrors
    {
        public static Error AlreadyCompleted(Guid clientId) => 
            Error.Problem(
            "Clients.AlreadyCompleted",
            $"The client item with Id = '{clientId}' is already completed.");

        public static Error LocationNotFound(Guid customerId) =>
            Error.NotFound("Ubicacion no encontrada", 
                $"La ubicacion del cliente con id '{customerId}' no fue encontrada");

        public static Error NotFound(Guid customerId) =>
            Error.NotFound(
            "Client.NotFound",
            $"The client item with the Id = '{customerId}' was not found");
    }
}
