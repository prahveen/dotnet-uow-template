using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Template.DAL.Repositories.Core
{
    public interface IBaseEntityRepository<T> where T : class, new()
    {
        /// <summary>
        /// Get all with include properties
        /// </summary>
        /// <param name="includeProperties">include properties</param>
        /// <returns></returns>
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get all with include properties asyncronously
        /// </summary>
        /// <param name="includeProperties">include properties</param>
        /// <returns></returns>
        Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get all 
        /// </summary>
        /// <param name="predicate">Lambda Expression</param>
        /// <param name="orderBy">Lambda Expression</param>
        /// <param name="numberofItems">integer</param>
        /// <param name="includeProperties">string array</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, IKey>> orderBy = null, bool IsAscending = true, int skip = 0, int numberofItems = 10, params string[] includeProperties);

        /// <summary>
        /// Get All ayncrously
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        T GetSingle(int id);

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="predicate">Predicat (which need more expressiongs)</param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="predicate">Predicat (which need more expressiongs)</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="predicate">Predicat (which need more expressiongs)</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate, params string[] includeProperties);

        /// <summary>
        /// Get single object asyncrously
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<T> GetSingleAsync(int id);

        /// <summary>
        /// Get Total Count
        /// </summary>
        /// <returns></returns>
        int GetTotalCount();

        /// <summary>
        /// GetFiltertedTotalCount
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int GetFiltertedTotalCount(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Search list
        /// </summary>
        /// <param name="predicate">Predicat (which need more expressiongs)</param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Search list asyncrously
        /// </summary>
        /// <param name="predicate">Predicat (which need more expressiongs)</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Detach Object
        /// </summary>
        /// <param name="entity">entity</param>
        void Detach(T entity);

        /// <summary>
        /// Add object to database
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Add object list to database
        /// </summary>
        /// <param name="entity"></param>
        void AddAll(ICollection<T> entity);

        /// <summary>
        /// Add object range to database
        /// </summary>
        /// <param name="entity"></param>
        void AddRange(List<T> entity);

        /// <summary>
        /// Add object range async to database
        /// </summary>
        /// <param name="entity"></param>
        void AddRangeAsync(List<T> entity);

        /// <summary>
        /// Edit object from database
        /// </summary>
        /// <param name="entity"></param>
        void Edit(T entity);

        /// <summary>
        /// Edit objects from database
        /// </summary>
        /// <param name="entity"></param>
        void EditAll(List<T> entity);

        /// <summary>
        /// Delete object from database
        /// </summary>
        /// <param name="entity">entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete multiple entities from databse
        /// </summary>
        /// <param name="predicate">predicate</param>
        void DeleteAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Final commit
        /// </summary>
        void Commit();
        void CommitAsynch();
        void DeleteAll(List<T> entity);

        //void AddAuditTrailRecord(EntityEntry dbEntityEntry, string function);
    }
}
