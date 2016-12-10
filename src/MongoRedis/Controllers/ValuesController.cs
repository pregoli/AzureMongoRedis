﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoRedis.Entities;
using MongoRedis.Services.People;
using MongoRedis.Services.RemoteCache;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Person>> Get()
        {
            var result = await _peopleService.GetAllAsync();

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Person> Get(string id)
        {
            var objectId = new ObjectId(id);

            return await _peopleService.GetByIdAsync(objectId);
        }

        // POST api/values
        [HttpPost]
        public async Task Post(Person person)
        {
            await _peopleService.AddAsync(person);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}