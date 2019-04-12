using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WinstonTraining.Web.Controllers.Api.Models;

namespace WinstonTraining.Web.Controllers.Api
{
    [RoutePrefix("api/developers")]
    public class DeveloperController : ApiController
    {
        private static List<Developer> _developersPuesdoDb = new List<Developer>()
        {
            new Developer(1, "Rob"),
            new Developer(2, "Max"),
            new Developer(3, "Ron"),
            new Developer(4, "Wes"),
            new Developer(5, "Victor")
        };

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllDevelopers()
        {
            var responseData = new
            {
                Developers = _developersPuesdoDb
            };

            return Ok(responseData);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetDeveloper(int id)
        {
            var matchingDev = _developersPuesdoDb.FirstOrDefault(developer => developer.Id == id);

            if (matchingDev == null)
                return NotFound();

            return Ok(matchingDev);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddDeveloper(Developer newDeveloper)
        {
            if (newDeveloper == null || newDeveloper.Id <= 0 || string.IsNullOrWhiteSpace(newDeveloper.Name))
                return BadRequest("The developer object is malformed or missing critical information.");

            if (_developersPuesdoDb.Any(existingDev => existingDev.Id == newDeveloper.Id))
                return BadRequest("This ID is already in use");

            _developersPuesdoDb.Add(newDeveloper);
            return Ok(newDeveloper);
        }

        [HttpPost]
        [Route("{name}")]
        public async Task<IHttpActionResult> AddDeveloperWithSequentialId(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is null or empty or whitespace.");

            var currentMaxId = _developersPuesdoDb.Max(developer => developer.Id);
            var newId = currentMaxId + 1;

            var newDeveloper = new Developer(newId, name);
            _developersPuesdoDb.Add(newDeveloper);
            return Ok(newDeveloper);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteDeveloper(int id)
        {
            if (id <= 0)
                return BadRequest("Id cannot be <= 0.");

            var matchingDeveloper = _developersPuesdoDb.FirstOrDefault(developer => developer.Id == id);

            if (matchingDeveloper == null)
                return NotFound();

            //rs: removet he developer from the list that should be deleted.
            _developersPuesdoDb = _developersPuesdoDb.Where(developer => developer.Id != matchingDeveloper.Id).ToList();
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateDeveloper(Developer updatedDeveloper)
        {
            if (updatedDeveloper == null || updatedDeveloper.Id <= 0 || string.IsNullOrWhiteSpace(updatedDeveloper.Name))
                return BadRequest("The developer object is malformed or missing critical information.");

            var developerToUpdate = _developersPuesdoDb.FirstOrDefault(developer => developer.Id == updatedDeveloper.Id);

            if (developerToUpdate == null)
                return NotFound();

            developerToUpdate.Name = updatedDeveloper.Name;
            return Ok(updatedDeveloper);
        }
    }
}