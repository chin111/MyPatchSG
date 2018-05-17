using System;
using System.Collections.Generic;
using System.IO;

using MyPatchSG.DL;
using MyPatchSG.DL.Models;
using MyPatchSG.BL;

namespace MyPatchSG.DAL
{
    public class UserDAO
    {
        DL.MyPatchSGDatabase db = null;
        protected static UserDAO sharedInstance;

        static UserDAO()
        {
            sharedInstance = new UserDAO();
        }

        protected UserDAO()
        {
            // instantiate the database
            db = MyPatchSG.DL.MyPatchSGDatabase.SharedInstance;
        }

        public static User GetUserById(int id)
        {
            return sharedInstance.db.GetItem<User>(id);
        }

        public static IEnumerable<User> GetUsers()
        {
            return sharedInstance.db.GetItems<User>();
        }
    }
}

