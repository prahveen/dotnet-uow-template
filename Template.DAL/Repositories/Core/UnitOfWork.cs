using System;
using System.Collections.Generic;
using System.Text;
using Template.DAL.Repositories.Abstract;
using Template.Entities.Context;
using Template.Entities.Entities;

namespace Template.DAL.Repositories.Core
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ApplicationDbContext _context;
        private ITemplateRepository _templateRepository ;
        private bool disposedValue = false; // To detect redundant calls

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public ITemplateRepository templateRepository
        {
            get
            {
                try
                {

                    return _templateRepository = _templateRepository ?? new TemplateRepository(_context);
                }
                catch (Exception ex)
                {
                    //NLogger<Customer>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                    throw ex;
                }
            }
        }

    public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               // NLogger<UnitOfWork>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {

            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
        }
        #endregion
    }
}
