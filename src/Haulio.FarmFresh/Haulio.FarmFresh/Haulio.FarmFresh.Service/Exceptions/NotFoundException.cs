using System;

namespace Haulio.FarmFresh.Service.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object key)
            : base($"Entity {entityName} ({key}) was not found.")
        {
        }
    }
}