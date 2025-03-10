using Microsoft.AspNetCore.Mvc;
using ActorReposLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActorsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private ActorReposList _actorReposList;
        public ActorsController(ActorReposList actorReposList) 
        {
            _actorReposList = actorReposList;
        }
        // GET: api/<ActorsController>
        [HttpGet]
        public IEnumerable<Actor> Get()
        {
            return _actorReposList.GetActors();
        }

        // GET api/<ActorsController>/5
        [HttpGet("{id}")]
        public Actor? Get(int id)
        {
            return _actorReposList.GetActorById(id);
        }

        // POST api/<ActorsController>
        [HttpPost]
        public Actor Post([FromBody] Actor NewActor)
        {
            return _actorReposList.Add(NewActor);

        }

        // PUT api/<ActorsController>/5
        [HttpPut("{id}")]
        public Actor? Put(int id, [FromBody] Actor NewActor)
        {
            return _actorReposList.UpdateActor(id, NewActor);
        }

        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public Actor Delete(int id)
        {
            return _actorReposList.Remove(id);
        }
    }
}
