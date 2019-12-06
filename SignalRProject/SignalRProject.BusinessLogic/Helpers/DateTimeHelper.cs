using System;
using SignalRProject.BusinessLogic.Helpers.Interfaces;

namespace SignalRProject.BusinessLogic.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public string FormatDateFromDb (DateTime date)
        {
            string response = date.ToString("MMMM dd, yyyy H:mm:ss");

            return response;    
        }
    }
}
