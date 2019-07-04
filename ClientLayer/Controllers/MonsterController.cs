using System.Net;
using Microsoft.AspNetCore.Mvc;
using TH.ClientLayer.Data.Monsters;
using TH.ClientLayer.Models;

namespace TH.ClientLayer.Controllers
{
    [Route("api/monsters")]
    [ApiController]
    public class MonsterController : Controller
    {
        private readonly IMonsterRepository _monsterRepository;

        public MonsterController(IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        [HttpGet]
        public JsonResult Monsters()
        {
            var models = _monsterRepository.All();
            return Result(HttpStatusCode.OK, models);
        }
        
        [HttpGet("{id}")]
        public JsonResult Monster(int id)
        {
            var model = _monsterRepository.Find(id);
            return Result(HttpStatusCode.OK, model);
        }
        
        [HttpPost]
        public JsonResult AddMonster([FromBody] Monster monster)
        {
            var model = _monsterRepository.Add(monster);
            return Result(HttpStatusCode.OK, model);
        }
                
        [HttpDelete("{id}")]
        public JsonResult RemoveMonster(int id)
        {
            _monsterRepository.Remove(id);
            return Result(HttpStatusCode.OK);
        }

        private JsonResult Result(HttpStatusCode status)
        {
            return Result(status, new {});
        }

        private JsonResult Result(HttpStatusCode status, dynamic model)
        {
            var result = new JsonResult(new { Success = true, Model = model }) 
            {
                StatusCode = (int) status
            };
            return result;
        }
    }
}