using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkPractices.Context;

namespace UnitOfWorkPractices.DAL
{
    public class UnitOfWork : IDisposable
    {
        private SchoolContext context = new SchoolContext();
        private GenericRepository<departament> departamentRepository;
        private GenericRepository<Course> courseRepository;

        public GenericRepository<departament> DeparatementoRepository
        {
            get
            {
                if (this.departamentRepository == null)
                {
                    this.departamentRepository = new GenericRepository<departament>(context);

                }
                return departamentRepository;
            }
        }
        public GenericRepository<Course> CourseRepository
        {
            get
            {
                if (this.CourseRepository == null)
                {
                    this.courseRepository = new GenericRepository<Course>(context);

                }
                return CourseRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
