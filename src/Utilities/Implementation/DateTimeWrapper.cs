
namespace Utilities.Implementation
{
    using System;
    using API;

    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
