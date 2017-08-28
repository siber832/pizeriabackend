using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pizzeria.Enums;
using Pizzeria.Database;
using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Facades
{
    public class FacadeUsers
    {

        public PizzeriaContext Context { set; private get; }

        public Errors Insert(String loguin, String password, String name, String surnames, String email) {  
            User user = new User() { Loguin = loguin, Password = password, Name = name, Surnames = surnames, Email = email };
              try
              {
                  Context.Users.Add(user);
                  Context.SaveChanges();
                  return Errors.NO_ERROR;
              }
              catch (Exception e)
              {
                    Context.Users.Remove(user);
                  return Errors.INSERTION_ERROR;
              }
           
        }

        public User GetUser(String loguin)
        {
            User user = Context.Users
                    .Where(u => u.Loguin.Equals(loguin))
                    .FirstOrDefault();
            return user;
        }
        
        public String GetId(String login)
        {
            return GetUser(login).UserId;
        }

        public Errors Loguin(String loguin, String password)
        {
            User user = GetUser(loguin);
            if (user == null) return Errors.LOGIN_DONT_EXISTS;
            if (!user.Password.Equals(password)) return Errors.PASSWORD_DONT_MATCH;
            return Errors.NO_ERROR;
        }

        public Errors Update(String oldLoguin, String loguin, String password, String name, String surnames)
        {
            User user = Context.Users
                    .Where(u => u.Loguin == oldLoguin)
                    .FirstOrDefault();

            if (!loguin.Equals("")) 
                user.Loguin = loguin;
            if(!password.Equals(""))
                user.Password = password;
            if(!name.Equals(""))
                user.Name = password;
            if(!surnames.Equals(""))
                user.Surnames = surnames;
            try
            {
                Context.Update(user);
                Context.SaveChanges();
                return Errors.NO_ERROR;
            } catch(Exception e)
            {
                return Errors.UPDATE_ERROR;
            }
            
        }
    }
}
