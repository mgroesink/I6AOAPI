using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using I6AOAPI.Models;

namespace I6AOAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private DB_A2A0BC_i6aoEntities db = new DB_A2A0BC_i6aoEntities();

        public IHttpActionResult GetStudents(string id)
        {
            string studentClass = db.Students.FirstOrDefault(s => s.StudentNr == id).Class;
            return Ok(db.getStudentsByClass(studentClass).Where(s=> s.StudentNr != id).ToList());
        }

        // GET: api/Students/5
        /// <summary>
        /// Gets info about the specified student.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(string id , string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Ok(db.Students.ToList());
            }
            else
            {
                getStudentInfo_Result student = db.getStudentInfo(id, code).Single();
                if (student == null)
                {
                    return NotFound();
                }

                return Ok(student);
            }
        }

 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(string id)
        {
            return db.Students.Count(e => e.StudentNr == id) > 0;
        }
    }
}