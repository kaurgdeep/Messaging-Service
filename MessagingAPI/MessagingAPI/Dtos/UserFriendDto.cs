﻿using MessagingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingAPI.Dtos
{
    public class UserFriendDto
    {
        public int UserFriendId { get; set; }
        public int FriendId { get; set; }
        public virtual User Friend { get; set; }


        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
