using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Template.Entities.Context;
using Template.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Template.DAL.Repositories.Core
{
    public class BaseEntityRepository<T> : IDisposable, IBaseEntityRepository<T> where T : class, IBaseEntity, new()
    {
        public ApplicationDbContext _context;
        //private readonly AuditTrailHelper auditTrailHelper = new AuditTrailHelper();

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        // Instantiate a SafeHandle instance.
       // private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public BaseEntityRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Add a record to database
        /// </summary>
        /// <param name="entity">object</param>
        public virtual void Add(T entity)
        {
            //using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            //{
                try
                {
                    EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                    _context.Set<T>().Add(entity);
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            //}
        }

        /// <summary>
        /// Add a collection of records to database
        /// </summary>
        /// <param name="entity collection">object</param>
        public virtual void AddAll(ICollection<T> entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<ICollection<T>>(entity);
                _context.Set<ICollection<T>>().Add(entity);
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Add a collection of records to database
        /// </summary>
        /// <param name="entity collection">object</param>
        public virtual void AddRange(List<T> entity)
        {
            try
            {
                _context.AddRange(entity);
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Add a collection of records to database
        /// </summary>
        /// <param name="entity collection">object</param>
        public virtual async void AddRangeAsync(List<T> entity)
        {
            try
            {
                await _context.AddRangeAsync(entity);
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get all with include
        /// </summary>
        /// <param name="includeProperties">include Properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get all with including asyncronuosly
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Edit entity of a the Database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Edit(T entity)
        {
            //Type type = entity.GetType();
            //AuditTrailHelper<T> auditTrailHelper = new AuditTrailHelper<T> ();

            //using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            //{
                try
                {
                    EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                
                dbEntityEntry.State = EntityState.Modified;

                //AddAuditTrailRecord(dbEntityEntry, "has been updated");
            }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            //}
        }

        /// <summary>
        /// Edit entities of a the Database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void EditAll(List<T> entity)
        {
            //using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            //{
                try
                {
                    _context.UpdateRange(entity);
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            //}
        }

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            //using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            //{
                try
                {
                    EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                //dbEntityEntry.State = EntityState.Modified;
                _context.Set<T>().Remove(entity);
                //AddAuditTrailRecord(dbEntityEntry, "has been deleted");
            }
            catch (Exception ex)
                {
                   // transaction.Rollback();
                    //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            //}
        }

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void DeleteAll(List<T> entity)
        {
            //using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            //{
                try
                {
                    EntityEntry dbEntityEntry = _context.Entry<List<T>>(entity);
                    dbEntityEntry.State = EntityState.Modified;
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            //}
        }

        /// <summary>
        /// Delete multiple entities from databse
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void DeleteAll(Expression<Func<T, bool>> predicate)
        {
            //using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            //{
                try
                {
                    IQueryable<T> query = _context.Set<T>().Where(predicate);

                    foreach (var item in query)
                    {
                        item.DisUse = true;
                        item.UpdatedTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            //}



        }


        /// <summary>
        /// Search objects Synchronously
        /// </summary>
        /// <param name="predicate">predicate for expressions</param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().Where(predicate);
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Search objects Asynchronously
        /// </summary>
        /// <param name="predicate">predicate for expressions</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get all objects
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get all objects by
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <param name="includeProperties">Include Properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, IKey>> orderBy = null, bool IsAscending = true, int skip = 0, int numberofItems = 10, params string[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includeProperties != null && includeProperties.Length != 0)
                {
                    if (includeProperties[0] != "")
                    {
                        foreach (var includeProperty in includeProperties[0].Split(","))
                        {
                            query = query.Include(includeProperty);
                        }
                    }
                }

                if (IsAscending && orderBy != null)
                {
                    return query.Where(predicate).OrderBy(orderBy).Skip(skip).Take(numberofItems).AsEnumerable();
                }
                else if (!IsAscending && orderBy != null)
                {
                    return query.Where(predicate).OrderByDescending(orderBy).Skip(skip).Take(numberofItems).AsEnumerable();
                }
                else if (predicate != null && orderBy == null)
                {
                    return query.Where(predicate).Skip(skip).Take(numberofItems).AsEnumerable();
                }
                else
                {
                    return query.Skip(skip).Take(numberofItems).AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get all for auto complete
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <param name="select">selection</param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> select, params string[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.Where(predicate).Select(select).AsEnumerable();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get all asyncrounsly
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
               // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetSingle(int id)
        {
            try
            {
                return _context.Set<T>().FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get single
        /// </summary>
        /// <param name="predicate">predicates</param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get single with includes
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return query.Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get single with includes
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includeProperties != null && includeProperties.Length != 0)
                {
                    if (includeProperties[0] != "")
                    {
                        foreach (var includeProperty in includeProperties[0].Split(","))
                        {
                            query = query.Include(includeProperty);
                        }
                    }
                }
                return query.Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// get single object asyncrounsly
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetSingleAsync(int id)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(x => x.ID == id);
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }

        }

        /// <summary>
        /// get total count
        /// </summary>
        /// <returns>int</returns>
        public int GetTotalCount()
        {
            try
            {
                return _context.Set<T>().Select(x => new { x.ID, x.DisUse }).Where(y => y.DisUse == false).Count();
            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }

        }

        /// <summary>
        /// GetFiltertedTotalCount
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int GetFiltertedTotalCount(Expression<Func<T, bool>> predicate)
        {

            try
            {
                IQueryable<T> query = _context.Set<T>();


                return query.Where(predicate).Count();



            }
            catch (Exception ex)
            {
                //NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Commit Changes Asynch
        /// </summary>
        public async void CommitAsynch()
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
                try
                {
                    await _context.SaveChangesAsync();
                    //scope.Complete();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                   // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                }
                catch (Exception ex)
                {
                   // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw;
                    throw new Exception(ex.Message);
                }
            //}
        }

        /// <summary>
        /// Commit Changes
        /// </summary>
        public void Commit()
        {
            int maxRetries = 3;
            int retryCount = 0;

            //using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Suppress))
            //{
                while (retryCount <= maxRetries)
                {
                    try
                    {
                        _context.SaveChanges();
                        retryCount = 4;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                        //var entry = ex.Entries.Single();
                        if (retryCount < maxRetries)
                        {
                            retryCount++;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    catch (TransactionAbortedException ex)
                    {
                       // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                        if (retryCount < maxRetries)
                        {
                            retryCount++;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    catch (RetryLimitExceededException ex)
                    {
                       // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                        if (retryCount < maxRetries)
                        {
                            retryCount++;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                       // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                        if (retryCount < maxRetries)
                        {
                            retryCount++;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    catch (TransactionException ex)
                    {
                       // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                        if (retryCount < maxRetries)
                        {
                            retryCount++;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
               // }
               // scope.Complete();
            }
        }

        /// <summary>
        /// Detach object
        /// </summary>
        /// <param name="entity">entity</param>
        public void Detach(T entity)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Suppress))
            //{
                try
                {
                    _context.Entry(entity).State = EntityState.Detached;
                    //scope.Complete();
                }
                catch (TransactionAbortedException ex)
                {
                   // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
                catch (ApplicationException ex)
                {
                   // NLogger<BaseEntity>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            //}
        }

        #region IDisposable Support
        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// Detach object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                //handle.Dispose();
                // Free any other managed objects here.
            }
            disposed = true;
        }
        #endregion

        //public void AddAuditTrailRecord(EntityEntry dbEntityEntry, string function)
        //{
        //    AuditTrail auditRecord = auditTrailHelper.AddAuditTrail(dbEntityEntry, function);
        //    _context.Set<AuditTrail>().Add(auditRecord);
        //    //Commit();
        //}
    }
}
