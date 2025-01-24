using System.ComponentModel.DataAnnotations;

namespace LeaderBoard.Model
{
    public class Score
    { 
        public int id { get; set; }
        public int user_id { get; set; }
        public int game_id { get; set; }
        public int score { get; set; }   
    }
}
