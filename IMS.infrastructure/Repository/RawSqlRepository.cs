using IMS.infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.infrastructure.Repository
{
    public  class RawSqlRepository:IRawSqlRepository
    {
        private readonly ImsDbContext _context;
        public RawSqlRepository(ImsDbContext context)
        {
            _context = context;

        }
        public IQueryable<TEntity>FromSql<TEntity>(string sql, params object[] parameters) where TEntity :class
        {
            var result = _context.Set<TEntity>().FromSqlRaw(sql, parameters);
            return result;
        }
    }
}
