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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ResultsController : ApiController
    {
        private DB_A2A0BC_i6aoEntities db = new DB_A2A0BC_i6aoEntities();

        /// <summary>
        /// Gets the result for the given student.
        /// </summary>
        /// <param name="id">The student identifier.</param>
        /// <param name="code">The verification code.</param>
        /// <returns></returns>
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetResult(string id, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Ok(db.Results.ToList());
            }
            else
            {
               List< getResults_Result> result = db.getResults(id).ToList();
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
        } 

 
        // POST: api/Results
        /// <summary>
        /// Adds a result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        [ResponseType(typeof(Result))]
        public IHttpActionResult PostResult(Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Results.Add(result);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResultExists(result.StudentNr))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = result.StudentNr }, result);
        }

 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultExists(string id)
        {
            return db.Results.Count(e => e.StudentNr == id) > 0;
        }
    }
}