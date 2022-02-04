using Core.Application.Contracts.Interfaces;
using System;

namespace Web.Framework.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
