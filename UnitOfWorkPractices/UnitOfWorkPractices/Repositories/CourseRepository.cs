using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkPractices.DAL;

namespace UnitOfWorkPractices.Repositories
{
    public class CourseRepository
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IEnumerable<Course> GetCourses()
        {
            var courses = unitOfWork.CourseRepository.Get(IncludeProperties: "Departament");
            return courses.ToList();        
        }
        public Course Details(int id)
        {
            Course course = unitOfWork.CourseRepository.GetByID(id);
            return Course;
        }

        public void Insert(Course course)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CourseRepository.Insert(course);
                unitOfWork.Save();
            }
        
        }
        public void Edit(Course course)
        {
            unitOfWork.CourseRepository.Update(course);
            unitOfWork.Save();
        
        }

        public void Delete(int id)
        {
            unitOfWork.CourseRepository.Delete(id);
            unitOfWork.Save();
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
