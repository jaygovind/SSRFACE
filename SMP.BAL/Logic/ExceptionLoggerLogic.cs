
namespace SMP.Bal.Logic
{

    using SMP.BAL.DTO;
    using SMP.BAL.ILogic;

    using SMP.DATA.Models;
    using SMP.Repository.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class ExceptionLoggerLogic : IExceptionLoggerLogic
    {
        #region Private properties
        private readonly IRepository<ExceptionLogger> _exceptionLoggerRepository;
        #endregion

        #region CTOR's

        public ExceptionLoggerLogic(IRepository<ExceptionLogger> exceptionLoggerRepository)
        {
            _exceptionLoggerRepository = exceptionLoggerRepository;
        }
        #endregion

        #region Interface IExceptionLoggerLogic Methods

        /// <summary>
        /// Save Exception
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> SaveExceptionObject(ExceptionLoggerDTO model)
        {
            try
            {
                ExceptionLogger Obj = this.MapDTOToModel(model);
                //Obj.CreatedBy = model.LoggedInUserId.Value;
                //Obj.CreatedDate = DateTime.UtcNow;
                //Obj.IsActive = true;
                var m = await _exceptionLoggerRepository.InsertAsync(Obj);
                await _exceptionLoggerRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Map Model To DTO
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private ExceptionLoggerDTO MapModelToDTO(ExceptionLogger u)
        {
            var modelDTO = new ExceptionLoggerDTO()
            {
                ID = u.ID,
                CallSite = u.CallSite,
                Message = u.Message,
                Level = u.Level,
                Logged = u.Logged,
                Logger = u.Logger,
                Application = u.Application,
                Exception = u.Exception
                //CreatedBy = u.CreatedBy,
                //IsActive = u.IsActive,
                //CreatedDate = u.CreatedDate,
                //LoggedInUserId = u.LoggedInUserId,
                //UpdatedBy = u.UpdatedBy,
                //UpdatedDate = u.UpdatedDate
            };
            return modelDTO;
        }

        /// <summary>
        /// Map DTO to Model
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private ExceptionLogger MapDTOToModel(ExceptionLoggerDTO u)
        {
            var model = new ExceptionLogger()
            {
                ID = u.ID,
                CallSite = u.CallSite,
                Message = u.Message,
                Level = u.Level,
                Logged = u.Logged,
                Logger = u.Logger,
                Application = u.Application,
                Exception = u.Exception
                //CreatedBy = u.CreatedBy,
                //IsActive = u.IsActive,
                //CreatedDate = u.CreatedDate,
                //LoggedInUserId = u.LoggedInUserId,
                //UpdatedBy = u.UpdatedBy,
                //UpdatedDate = u.UpdatedDate
            };
            return model;
        }

        /// <summary>
        /// Map List
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<ExceptionLoggerDTO> MapObjListToDTOList(List<ExceptionLogger> list)
        {
            var returnList = new List<ExceptionLoggerDTO>();
            foreach (var item in list)
            {
                returnList.Add(MapModelToDTO(item));
            }
            return returnList;
        }

        #endregion
    }
}
