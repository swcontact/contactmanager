using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _context;

        // dependency Injection
        public PersonController(PersonContext context)
        {
            _context = context;

            if (_context.Persons.Count() < 20)
            {
                _context.Persons.Add(new Person() {
                     FirstName="Huey"+ _context.Persons.Count(),
                     LastName="Zhou",
                     Birthday = new DateTime(1999,1,2),
                     Email = "hueuzhou@hotmail.com",
                     Telephone = "6479292569"
              
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Person>> GetAll()
        {
            return _context.Persons.ToList();
        }

        [HttpGet("{id}", Name ="GetPerson")]
        public ActionResult<Person> GetById(long id)
        {
            var person = _context.Persons.Find(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

    }
}