using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseControlLib
{
    //通用User字段
    public class User
    {
        public string Username  //系统登录用户名
        {
            get;
            set;
        }

        public string Password  //系统登录密码
        {
            get;
            set;
        }

        public bool CanLogin    //是否可以登录系统
        {
            get;
            set;
        }

        public User(string username, string password, bool canlogin)
        {
            Username = username;
            Password = password;
            CanLogin = canlogin;
        }
    }
}
