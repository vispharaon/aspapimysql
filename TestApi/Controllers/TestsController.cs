using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using TestdbModel;

namespace TestApi.Controllers
{
    public class TestsController : ApiController
    {
        private readonly TestdbEntities _db = new TestdbEntities();

        // GET: api/Tests
        public IEnumerable<string> Get()
        {
            var tests = _db.Tests;
            var testsJson = new List<string>();
            foreach (var test in tests)
                 testsJson.Add(JsonConvert.SerializeObject(test));

            return testsJson;
        }

        // GET: api/Tests/5
        public string Get(int id)
        {
            var test = _db.Tests.FirstOrDefault(x => x.Id == id);
            return test != null ? JsonConvert.SerializeObject(test) : "";
        }

        // POST: api/Tests
        public void Post([FromBody]string value)
        {
            var test = (Test) JsonConvert.DeserializeObject(value);
            _db.Tests.AddObject(test);
            _db.SaveChanges();
        }

        // PUT: api/Tests/5
        public void Put(int id, [FromBody]string value)
        {
            var test = _db.Tests.FirstOrDefault(x => x.Id == id);
            if(test == null) throw new Exception($"Test with id {id} does not exist.");

            var testRequest = (Test)JsonConvert.DeserializeObject(value);
            test.IsOk = testRequest.IsOk;
            test.Name = testRequest.Name;
            _db.SaveChanges();
        }

        // DELETE: api/Tests/5
        public void Delete(int id)
        {
        }
    }
}
