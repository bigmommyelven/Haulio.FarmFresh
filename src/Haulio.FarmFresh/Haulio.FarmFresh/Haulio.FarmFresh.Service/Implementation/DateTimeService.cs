using Haulio.FarmFresh.Service.Contract;
using System;

namespace Haulio.FarmFresh.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}