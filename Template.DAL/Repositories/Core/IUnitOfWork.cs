using Template.DAL.Repositories.Abstract;
using Template.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.DAL.Repositories.Core
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Create Customer Repository
        /// </summary>
        ITemplateRepository templateRepository { get; }


        /// <summary>
        /// Save Changes
        /// </summary>
        void Save();

    }
}
