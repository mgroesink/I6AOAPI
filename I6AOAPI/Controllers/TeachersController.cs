using DemoWebAPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWebAPI2.Controllers
{
    public class TeachersController : ApiController
    {
        static List<Teacher> teachers = new List<Teacher>();

        public TeachersController()
        {
            if (teachers.Count == 0)
            {
                teachers.Add(new Teacher { FirstName = "M.", Id = 1, LastName = "Roesink" });
                teachers.Add(new Teacher { FirstName = "G.", Id = 2, LastName = "Wargers" });
                teachers.Add(new Teacher { FirstName = "M.", Id = 3, LastName = "Jansink" });
                teachers.Add(new Teacher { FirstName = "L.", Id = 4, LastName = "Gojani" });
                teachers.Add(new Teacher { FirstName = "V.", Id = 5, LastName = "Blokhuis" });
                teachers.Add(new Teacher { FirstName = "A.", Id = 6, LastName = "van der Kaaden" });
                teachers.Add(new Teacher { FirstName = "B.", Id = 7, LastName = "van de Woord" });
            }
        }

        // GET: api/People
        /// <summary>
        /// Gets a list of teachers.
        /// </summary>
        /// <returns></returns>
        public List<Teacher> Get()
        {
            return teachers;
        }

        // GET: api/People/5
        /// <summary>
        /// Gets info about one teacher.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Teacher Get(int id , string code)
        {
            return teachers.FirstOrDefault(p => p.Id == id);
        }

    }
}
