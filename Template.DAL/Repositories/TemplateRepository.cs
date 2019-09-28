using Template.DAL.Repositories.Abstract;
using Template.DAL.Repositories.Core;
using Template.Entities.Context;
using Template.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Template.DAL.Repositories
{
   public class TemplateRepository : BaseEntityRepository<DefaultEntitiy>, ITemplateRepository
    {
        private new ApplicationDbContext _context;
        public TemplateRepository(ApplicationDbContext context) : base(context)
        {
            try
            {
                this._context = context;
            }
            catch (Exception ex)
            {
                //NLogger<Country>.Error(Constants.ExceptionPolicy.DataAccess, ex);
                throw ex;
            }
        }

        public DefaultEntitiy GetSingleByName(string name)
        {
            try
            {
                return _context.Set<DefaultEntitiy>().FirstOrDefault(x => x.Name == name);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
