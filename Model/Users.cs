using System.ComponentModel.DataAnnotations;

namespace LeaderBoard.Model
{
    public class Users
    { 
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; } 
        public bool is_admin { get; set; }
        [EmailAddress]
        public string email { get; set; }   
    }
}
