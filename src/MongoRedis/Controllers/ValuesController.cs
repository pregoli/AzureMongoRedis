using Microsoft.AspNetCore.Mvc;
using MongoRedis.Entities;
using MongoRedis.Services.People;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoRedis.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IPeopleService _peopleService;

        public ValuesController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _peopleService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            ObjectId objectId;
            if(ObjectId.TryParse(id, out objectId))
                return Ok(await _peopleService.GetByIdAsync(id));

            return BadRequest("Invalid objectid mate!");
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Person person)
        {
            if(person != null)
                return Ok(await _peopleService.AddAsync(person));

            return BadRequest("Invalid person mate!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Person person)
        {
            ObjectId objectId;
            if (ObjectId.TryParse(id, out objectId))
                return Ok(await _peopleService.UpdateAsync(objectId, person));

            return BadRequest("Invalid objectid mate!");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            ObjectId objectId;
            if (ObjectId.TryParse(id, out objectId))
                return Ok(await _peopleService.RemoveAsync(objectId));

            return BadRequest("Invalid objectid mate!");
        }
    }
}
