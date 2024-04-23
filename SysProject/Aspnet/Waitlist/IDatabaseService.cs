using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Waitlist.EntityModels;

namespace Waitlist
{
    public interface IDatabaseService
    {
        Task<bool> AddStudentData(string firstName, string lastName, string email);

        // method to get course data
        List<CourseDatum> GetCourseData();
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly TestDbContext _context;
        
        // constructor to get access to the database context
        public DatabaseService(TestDbContext context)
        {
            _context = context;
        }

        public List<CourseDatum> GetCourseData()
        {
            // get the lines of data for the courses contained in the CourseData table
            var courseData = _context.CourseData.ToList();
            return courseData;

        }

        public async Task<bool> AddStudentData(string firstName, string lastName, string email)
        {
            try
            {
                var dbResult = _context.StudentData.FirstOrDefault();
                if (dbResult == null)
                {
                    // add the student data to the database
                    _context.StudentData.Add(new StudentDatum
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        EmailAddress = email
                    });

                }
                else
                {
                    dbResult.FirstName = firstName;
                    dbResult.LastName = lastName;
                    dbResult.EmailAddress = email;
                }

                // save changes to the database
                await _context.SaveChangesAsync();
            }

            catch
            {
                return false;
            }

            return true;

        }
    }
}
