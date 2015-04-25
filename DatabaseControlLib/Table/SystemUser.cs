using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseControlLib
{
    public class SystemUser : User
    {
        public int UID
        {
            get;
            set;
        }

        public SystemUser(int uid, string username, string password, bool canlogin)
            : base(username, password, canlogin)
        {
            UID = uid;
        }
    }
}
