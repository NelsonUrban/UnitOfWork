using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitOfWorkPractices.Context;

namespace UnitOfWorkPractices.DAL
{
    public class GenericRepository<Tentity>where Tentity :class
    {  

        internal SchoolContext _context;
        internal DbSet<Tentity> _dbset;

        public GenericRepository(SchoolContext context)
        {

            this._context = context;
            this._dbset = context.set<Tentity>();
        }

        public virtual IEnumerable<Tentity> Get(
            Expression<Func<Tentity, bool>> filter = null, Func<IQueryable<Tentity>, IOrderedQueryable<Tentity>> orderBy = null,
            string IncludeProperties = "")
        {
            IQueryable<Tentity> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var IncludeProperty in IncludeProperties.Split( new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(IncludeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();            
            }
        }

        public virtual Tentity GetByID(object id)
        {
            return _dbset.Find(id);
        
        }
        public virtual void Insert(Tentity tentity)
        {
            this._dbset.Add(tentity);        
        }

        public virtual void Delete(object id)
        {
            Tentity tentityToDelete = _dbset.Find(id);
            Delete(tentityToDelete);
        
        }
        public virtual void Delete(Tentity entityToDelete)
        {
            if (_context.Entry(entityToDelete).state == EntityState.Detached)
            {
                _dbset.Attach(entityToDelete);
            }
            _dbset.Remove(entityToDelete);
         }

        public virtual void Update(Tentity entityToUpdate)
        {
            _dbset.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).state = EntityState.Modified;
        }
    }
}
