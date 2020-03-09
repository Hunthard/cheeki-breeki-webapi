using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheekiBreekiWebAPI;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheekiBreekiWebAPI.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Player>> Get()
        {
            Leaderboard.leaderboard.Sort(new ScoreComparer());

            string json = JsonSerializer.Serialize<List<Player>>(Leaderboard.leaderboard);
            return new ObjectResult(json);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(Object value)
        {
            byte[] tmp = new System.Text.UTF8Encoding().GetBytes(value.ToString());
            Player data = JsonSerializer.Deserialize<Player>(tmp);
            
            if (data != null)
            {
                Leaderboard.leaderboard.Add(data);

                if (Leaderboard.leaderboard.Count > 10)
                {
                    Leaderboard.leaderboard.Sort(new ScoreComparer());
                    Leaderboard.leaderboard.RemoveAt(Leaderboard.leaderboard.Count - 1);
                }
                
                return new ObjectResult(data);
            }
            else
            {
                return BadRequest();
            }
        }
    }

    public class ScoreComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (x.Score < y.Score)
                return 1;
            else if (x.Score > y.Score)
                return -1;
            else
                return 0;
        }
    }
}
