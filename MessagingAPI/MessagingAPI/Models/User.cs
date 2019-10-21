using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Created { get; set; }
        [InverseProperty("User")]
        public List<UserFriend> UserFriends { get; set; }

        public User()
        {
            UserFriends = new List<UserFriend>();
            //Posts = new List<Post>();
            
        }
    }
}
