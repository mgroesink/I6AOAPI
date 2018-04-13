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

        //// GET: api/Students
        ///// <summary>
        ///// Gets a list of students in the same class.
        ///// </summary>
        ///// <param name="snr">The SNR.</param>
        ///// <returns></returns>
        //public List<getStudentsByClass_Result> GetStudents(string snr)
        //{
        //    string studentClass = db.Students.FirstOrDefault(s => s.StudentNr == snr).Class;
        //    return db.getStudentsByClass(studentClass).ToList();
        //}

        public List<Student> GetStudents()
        {
            //string studentClass = db.Students.FirstOrDefault(s => s.StudentNr == snr).Class;
            return db.Students.ToList();
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
            getStudentInfo_Result student = db.getStudentInfo(id , code).Single();
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(string id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentNr)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        /// <summary>
        /// Update the specified student.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(student.StudentNr))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = student.StudentNr }, student);
        }

        // DELETE: api/Students/5
        /// <summary>
        /// Deletes the specified student.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Student))]
        [Authorize]
        public IHttpActionResult DeleteStudent(string id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
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