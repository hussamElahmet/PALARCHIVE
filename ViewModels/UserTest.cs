using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace marble.ViewModels
{
    public class User
    {
        public int UserId { set; get; }
        public string userName{ set; get; }
        public string password{ set; get; }
        public string role { set; get; }
}

    public class UserInit
    {
        public static List<User> Init()
        {
            return new List<User>
            {
                new User{UserId=1,userName="ahmed",password="ahmed@#1234",role="A"}
            };
        }
    }
}