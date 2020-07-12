
using SMP.BAL.DTO;
using System.Threading.Tasks;

namespace SMP.BAL.ILogic
{
    public interface IExceptionLoggerLogic
    {
        Task<bool> SaveExceptionObject(ExceptionLoggerDTO obj);
    }
}
