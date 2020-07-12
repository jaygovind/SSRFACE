using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Repository.Repository
{
    public interface IRepository<T> where T : class
    {
        int? Update(T entity, params Expression<Func<T, object>>[] properties);
        int? InsertAndGetId(T entity);
        void Add(T entities);



        IEnumerable<T> GetAll();
        Task<T> GetAsync(long id);
        Task<int?> InsertAsync(T entity);
        int? Insert(T entity);
        Task<int?> UpdateAsync(T entity);
        Task<int?> DeleteAsync(T entity);
        void Remove(T entity);
        Task<int?> SaveChangesAsync();
        int? SaveChanges();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<int?> UpdateAsync(List<T> entity);
        Task<int?> InsertMultiAsync(List<T> entity);
        Task<T> ExecuteWithJsonResultAsync(string name, string parserString, params SqlParameter[] parameters);
        List<T> ExecuteWithJsonResult(string name, string parserString, params SqlParameter[] parameters);
        void ExecuteStoreProcedureWithoutReturnType(string procName, string entity, params SqlParameter[] parameters);
        T Find(params object[] keyValues);


        void AddRange(IEnumerable<T> entities);

        void RemoveRange(IEnumerable<T> entities);

        DbDataReader ExecStoreProcedureOnly(string procName, string entity, params SqlParameter[] parameters);


        List<T> ExecuteSP(string query, params object[] parameters);

        List<T> ExecStoreProcedureWithReturnType(string name, string parserString, params SqlParameter[] parameters);
    }
}
