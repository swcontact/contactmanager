using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Cors;
using NLog;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactContext _context;
        private Logger _logger;
        private bool _debug = true;

        public ContactsController(ContactContext context)
        {
            _context = context;
            _logger = LogManager.GetCurrentClassLogger();

            debugLog("Initialized Contacts Controller");
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Person> GetContacts()
        {
            debugLog("GetContents started");

            IEnumerable<Person> contacts = _context.Contacts;
            foreach (Person person in contacts)
            {
                ExtractContact(person);
            }

            debugLog("GetContents ended");

            return contacts;
        }

        // GET: api/Contacts/page/1?size=10
        [HttpGet]
        [Route("page/{page}")]
        public async Task<IActionResult> Page([FromRoute] int page, int size = 5)
        {
            debugLog("Page loading started");

            if (page < 1)
            {
                return BadRequest();
            }

            var count = await _context.Contacts.CountAsync();
            var contacts = await _context.Contacts.OrderBy(s => s.FirstName).Skip((page - 1) * size).Take(size).ToListAsync();

            if (contacts == null)
            {
                return NotFound();
            }

            foreach (Person person in contacts)
            {
                ExtractContact(person);
            }

            debugLog("Page loading ended");

            return Ok(new { Contacts = contacts, Count = count });
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] long id)
        {
            debugLog("GetContent started");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            ExtractContact(contact);

            debugLog("GetContent ended");

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] long id, [FromBody] Person contact)
        {
            debugLog("PutContent started");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.ID)
            {
                return BadRequest();
            }

            SetContact(contact);

            _context.Entry(contact).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            debugLog("PutContent ended");

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Person contact)
        {
            debugLog("Post contact started");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            SetContact(contact);

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            debugLog("Post contact ended");

            return CreatedAtAction("GetContact", new { id = contact.ID }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] long id)
        {
            debugLog($"Delete contact started: ({id})");

            if (!ModelState.IsValid)
            {
                debugLog("Bad request for Delete ", "error");

                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                debugLog($"Delete contact({id}) is not found!");

                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            debugLog($"Delete contact ({id}) ended.");

            return Ok(contact);
        }

        private bool ContactExists(long id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }

        private void SetContact(Person contact)
        {
            switch (contact.Category) {
                case Person.Customer:
                    contact.Contact = contact.Email + (string.IsNullOrEmpty(contact.Birthday) ? "" : ", " + contact.Birthday);
                    break;
                case Person.Supplier:
                    contact.Contact = contact.Telephone;
                    break;
                default:
                    contact.Contact = "";
                    break;
            }
        }

        private void ExtractContact(Person contact)
        {
            switch (contact.Category)
            {
                case Person.Customer:
                    string[] emailBirth = (contact.Contact + ",,").Split(",");
                    contact.Email = emailBirth[0];
                    contact.Birthday = emailBirth[1];
                    break;
                case Person.Supplier:
                    contact.Telephone = contact.Contact;
                    break;
                default:
                    break;
            }
        }

        private void debugLog(string message, string logType = "info", bool debug = true)
        {
            if (_debug)
            {
                switch (logType)
                {
                    case "info":
                        _logger.Info(message);
                        break;
                    case "error":
                        _logger.Error(message);
                        break;
                    default:
                        _logger.Warn(message);
                        break;
                }
            }
        }
    }
}