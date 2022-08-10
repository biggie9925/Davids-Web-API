using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;


namespace Davids_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private static List<Player> players = new List<Player>
            {
                //created a list of 5 Player objects for testing as no DB was used 
                new Player {
                    Id = 0,
                    Number = 12,
                    Name = "Tom",
                    Avg_points = 15,
                    Avg_rebs = 15,
                    Avg_asts = 5,
                },

                new Player {
                    Id = 1,
                    Number = 24,
                    Name = "Jerry",
                    Avg_points = 25,
                    Avg_rebs = 16,
                    Avg_asts = 3,
                },

                new Player {
                    Id = 2,
                    Number = 6,
                    Name = "Mickey Mouse",
                    Avg_points = 30,
                    Avg_rebs = 10,
                    Avg_asts = 5,
                },

                new Player {
                    Id = 3,
                    Number = 24,
                    Name = "Bugs Bunny",
                    Avg_points = 20,
                    Avg_rebs = 5,
                    Avg_asts = 3
                },

                new Player {
                    Id = 4,
                    Number = 7,
                    Name = "Road Runner",
                    Avg_points = 35,
                    Avg_rebs = 16,
                    Avg_asts = 8
                }
            };

        ///<summary>
        ///Get all Local Players
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<List<Player>>> Get()
        {
            foreach (Player player in players)
            {
                //calling the API multiple times, which means it takes a while to load
                //best practice would be to create an array/list of external API data and then assign it in the loop
                //unfortunately ran out of time to implement this
                Get(player.Id);
            }

            return Ok(players);
        }

        /// <summary>
        /// Search for Local Player by ID and see Local Player's favourite (random) NBA Player
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = players.Find(p => p.Id == id);

            Random random_number = new Random();
            int player_id = random_number.Next(1, 3092);
            string url = $"https://www.balldontlie.io/api/v1/players/{player_id}";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string json = (new WebClient()).DownloadString(url);
            var nba_player_data = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(json);
            player.Fav_player_name = nba_player_data.first_name + " " + nba_player_data.last_name;

            if (player == null)
                return BadRequest("Player not found.");

            return Ok(player);
        }


        /// <summary>
        /// Add Local API player
        /// </summary>
        /// <returns>A 201 Created response</returns>
        [HttpPost]
        public async Task<ActionResult<List<Player>>> AddPlayer(Player player)
        {
            players.Add(player);
            return Ok(players);
        }


        /// <summary>
        /// Update Local API Player
        /// </summary>
        /// <returns>A 201 Created Response></returns>
        [HttpPut]
        public async Task<ActionResult<List<Player>>> UpdatePlayer(Player request)
        {
            var player = players.Find(p => p.Id == request.Id);
            if (player == null)
                return BadRequest("Player not found.");

            player.Number = request.Number;
            player.Name = request.Name;
            player.Avg_points = request.Avg_points;
            player.Avg_rebs = request.Avg_rebs;
            player.Avg_asts = request.Avg_asts;

            return Ok();
        }


        /// <summary>
        /// Delete Local API Player
        /// </summary>
        /// <returns>A 204 No Content Response</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Player>>> Delete(int id)
        {
            var player = players.Find(p => p.Id == id);
            if (player == null)
                return BadRequest("Player not found.");

            players.Remove(player);
            return Ok(players);

        }
    }
}
