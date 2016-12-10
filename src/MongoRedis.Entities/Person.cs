using MongoRedis.Entities.Base;
using System.Collections.Generic;

namespace MongoRedis.Entities
{
    public class Person : Entity
    {
        public Person()
        {
            this.Animals = new List<Animal>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
