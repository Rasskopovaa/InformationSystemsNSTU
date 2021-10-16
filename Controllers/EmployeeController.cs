 
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using lab1.Models;
using lab1.Storage;
using Serilog;


namespace lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
         private IStorage<Employee> _memCache;

        public ValuesController(IStorage<Employee> memCache)
        {
            _memCache = memCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("No such");
            Log.Debug($"Such employee with ${id} is not exist");

            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _memCache.Add(value);
            
             Log.Information($"Employee ${value.ToString()} is added");
             Log.Debug($"Full info about employee: ${value}");
            return Ok($"{value.ToString()} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Employee value)
        {
            if (!_memCache.Has(id)) return NotFound("No such");

            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _memCache[id];
            _memCache[id] = value;

             Log.Information($"Employee with ${id} id is updated");
             Log.Debug($"Full info about employee: ${value}");

            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("No such");

            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);

             Log.Information($"Employee with ${id} id was deleted");
             

            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}