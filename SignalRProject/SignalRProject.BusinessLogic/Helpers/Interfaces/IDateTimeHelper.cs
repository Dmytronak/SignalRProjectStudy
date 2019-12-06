using System;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Helpers.Interfaces
{
    public interface IDateTimeHelper
    {
        string FormatDateFromDb (DateTime date);
    }
}
