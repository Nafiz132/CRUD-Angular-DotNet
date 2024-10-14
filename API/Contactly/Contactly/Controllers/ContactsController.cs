using Contactly.Data;
using Contactly.Models;
using Contactly.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contactly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactlyDbContext _dbContext;

        public ContactsController(ContactlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = _dbContext.Contacts.ToList();
            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult AddContact(AddContactRequestDTO request)
        {
            var domainModelContact = new Contact
            {
                Id = new Guid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Favourite = request.Favourite
            };
            _dbContext.Add(domainModelContact);
            _dbContext.SaveChanges();
            return Ok(domainModelContact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = _dbContext.Contacts.Find(id);
            if(contact is not null)
            {
                _dbContext.Contacts.Remove(contact);
                _dbContext.SaveChanges();
            }
            return Ok();
        }
    }
}
