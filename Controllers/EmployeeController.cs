using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using lab1.Models;

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

        private static List<Employee> _employee = new List<Employee>();
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return Ok(_employee);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            if (_employee.Count <= id) return NotFound("Такого нет");
            return Ok(_employee[id]);
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Employee value)
        {
             var validationResult = value.Validate();
             if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

             _employee.Add(value);

            return Ok($"{value.ToString()} был добавлен");
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee value)
        {  
            if (_employee.Count <= id) return NotFound("Такого нет");
           var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _employee[id];
          _employee[id]=value;
          return Ok($"{previousValue.ToString()} был обновлён {value.ToString()}");
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_employee.Count <= id) return  NotFound("Такого нет");
            var valueToRemove = _employee[id];
            _employee.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} был удалён");
        }
    }
}
