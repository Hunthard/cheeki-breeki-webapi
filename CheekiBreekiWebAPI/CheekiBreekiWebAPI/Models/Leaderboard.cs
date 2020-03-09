using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CheekiBreekiWebAPI
{
    public class Player : IComparable
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("score")]
        public int Score { get; set; }
        [JsonPropertyName("playedTime")]
        public float PlayedTime { get; set; }

        public int CompareTo(object obj)
        {
            return Score.CompareTo(obj);
        }
    }

    public static class Leaderboard
    {
        public static List<Player> leaderboard { get; set; }

        static Leaderboard()
        {
            leaderboard = new List<Player>();
        }
    }
}