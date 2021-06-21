using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Singlestone.Models;

namespace Singlestone.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactContext _context;

        public ContactsController(ContactContext context)
        {
            _context = context;
        }
        [HttpGet("call-list")]
        public async Task<ActionResult<IEnumerable<CallList>>> GetCallList()
        {
            var contact = (from c in _context.Contacts
                           select new Contact{ContactId=c.ContactId, 
                                              Name = c.Name, 
                                              Phone = c.Phone }).ToList();
            List<CallList> callList = new List<CallList>();
            
            foreach(var i in contact)
            {
                foreach(var p in i.Phone)
                {
                    if(p.Type =="home")
                    {
                        CallList cl = new CallList
                        {
                            Name = i.Name,
                            Number = p.Number
                        };
                        callList.Add(cl);
                        break;
                    }
                }
            }
            return  callList.ToArray();
        }
        // GET: api/Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contact = (from c in _context.Contacts
                           select new { Name = c.Name, 
                                        Address = c.Address, 
                                        Phone = c.Phone }).ToList();
         
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            Contact contact = ((from c in _context.Contacts
                                where c.ContactId == id
                                select new Contact{ ContactId = c.ContactId,
                                                    Name = c.Name, 
                                                    Address = c.Address, 
                                                    Phone = c.Phone, 
                                                    Email = c.Email }).FirstOrDefault());
                   
            if (contact == null)
            {
                return NotFound();
            }

           return  contact;
        }
        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact modified)
        {
            Contact contact = ((from c in _context.Contacts
                                where c.ContactId == id
                                select new Contact{ContactId = c.ContactId,
                                                   Name = c.Name,
                                                   Address = c.Address,
                                                   Phone = c.Phone,
                                                   Email = c.Email}).FirstOrDefault());

            contact.Name = modified.Name;
            contact.Address = modified.Address;
            foreach(var p in contact.Phone)
            {
                foreach (var m in modified.Phone)
                {
                    if (m.Type == p.Type)
                        p.Number = m.Number;
                }
            }
             contact.Email = modified.Email;
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

            return NoContent();
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Contacts.Add(contact);
                       
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContact), new { id = contact.ContactId }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            Contact contact = ((from c in _context.Contacts
                                where c.ContactId == id
                                select new Contact
                                {
                                    ContactId = c.ContactId,
                                    Name = c.Name,
                                    Address = c.Address,
                                    Phone = c.Phone,
                                    Email = c.Email
                                }).FirstOrDefault());
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactId == id);
        }
    }
}
