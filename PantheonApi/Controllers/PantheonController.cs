using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PantheonApi.Models;

namespace PantheonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PantheonController : ControllerBase
    {
        private readonly PantheonContext _context;

        public PantheonController(PantheonContext context)
        {
            _context = context;

            if (_context.PantheonItems.Count() == 0)
            {
                _context.PantheonItems.Add(new PantheonItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<PantheonItem>> GetAll()
        {
            return _context.PantheonItems.ToList();
        }

        [HttpGet("{id}", Name = "GetPantheon")]
        public ActionResult<PantheonItem> GetById(long id)
        {
            var item = _context.PantheonItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(PantheonItem item)
        {
            _context.PantheonItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPantheon", new { id = item.Id }, item);
        }   

        [HttpPut("{id}")]
        public IActionResult Update(long id, PantheonItem item)
        {
            var pantheon = _context.PantheonItems.Find(id);
            if (pantheon == null)
            {
                return NotFound();
            }

            pantheon.CountryOrigin = item.CountryOrigin;
            pantheon.Name = item.Name;

            _context.PantheonItems.Update(pantheon);
            _context.SaveChanges();
            return NoContent();
        }    

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var pantheon = _context.PantheonItems.Find(id);
            if (pantheon == null)
            {
                return NotFound();
            }

            _context.PantheonItems.Remove(pantheon);
            _context.SaveChanges();
            return NoContent();
        }
    }
}