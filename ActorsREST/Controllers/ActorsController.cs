using Microsoft.AspNetCore.Mvc;
using ActorReposLib;
using ActorsREST.Records;
using Microsoft.AspNetCore.Cors;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // GET: api/<ActorsController>
        
        [HttpGet]
        [EnableCors("AllowAll")]
        public ActionResult<IEnumerable<Actor>> GetActors(
            [FromQuery] string? name,
            [FromQuery] string? sortby,
            [FromQuery] int birthyearbefore,
            [FromQuery] int birthyearafter)
        {
            IEnumerable<Actor> result = _actorReposList.GetActors(Birthyearbefore:birthyearbefore,Birthyearafter:birthyearafter,name:name,sortBy:sortby);
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        // GET api/<ActorsController>/5
        
        [HttpGet("{id}")]
        [EnableCors("AllowAll")]
        public ActionResult<Actor> Get(int id)
        {
            Actor? actor = _actorReposList.GetActorById(id);
            if(actor != null)
            {
                return Ok(actor);
            }
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST api/<ActorsController>
        
        [HttpPost]
        
        public ActionResult<Actor> Post([FromBody] ActorRecord NewActorRecord)
        {
            try
            {
                Actor actorConverted = RecordHelper.ConvertActorRecord(NewActorRecord);
                Actor actor = _actorReposList.Add(actorConverted);
                return Created("*/*" + actorConverted.Id, actor);
            }
            catch(ArgumentNullException ex) 
            {
                return BadRequest("Indeholder nulls" + ex.Message);
            } 
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest("Out of range" + ex.Message);
            }
            catch(ArgumentException ex) 
            {
                return BadRequest("" + ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT api/<ActorsController>/5
        [HttpPut("{id}")]
        public ActionResult<Actor> Put(int id, [FromBody] ActorRecord NewActorRecord)
        {
            try
            {
                Actor actorConverted = RecordHelper.ConvertActorRecord(NewActorRecord);
                Actor? updated = _actorReposList.UpdateActor(id, actorConverted);

                if(updated != null)
                {
                    return Ok(updated);
                }
                return NotFound();
                
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Indeholder nulls" + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Out of range" + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("" + ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Actor> Delete(int id)
        {
            Actor deleted = _actorReposList.Remove(id);
            if(deleted != null)
            {
                return Ok(deleted);

            }
            return NotFound();
        }
    }
}
