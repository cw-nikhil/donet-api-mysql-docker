using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace demo.Controllers
{
    [ApiController]
    [Route("api/")]
    public class DemoController : ControllerBase
    {
        private static readonly Dictionary<string, string> _mapping = new Dictionary<string, string>();
        private static readonly Repo _repo = new Repo();
        public DemoController()
        {
        }
        [HttpGet]
        [Route("get/{key}/")]
        public ActionResult Get(string key)
        {
            string value = _repo.GetValue(key);
            if (value == string.Empty)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        [Route("add/")]
        public ActionResult AddKeyValue(KVP kvp)
        {
            string key = kvp.Key;
            string value = kvp.Value;
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                object error = new
                {
                    message = "key or value invalid"
                };
                return BadRequest(error);
            }
            int id = _repo.Add(kvp);
            if (id > 0)
            {
                return Ok("created");
            }
            return BadRequest("Duplicate key");
        }
    }
}
