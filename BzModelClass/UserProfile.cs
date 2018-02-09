using DBModelClass.DBModel;
using KoyoSMS.WCF.Common;
using KoyoSMS.WCF.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BzModelClass
{
    public class UserProfile
    {
        //EFDBModelEntities db = new EFDBModelEntities();
        
        protected EFDBModelEntities db
        {
            get;
            private set;
        }
        public UserProfile()
        {
            this.db = new EFDBModelEntities();
        }

        public List<Users> getUserList(string username, string password)
        {
            IQueryable<Users> query = (from a in db.Users
                                       where a.Username == username && a.Password == password
                                         select a
                                         );
            //query.Count();
            return query.ToList();
        }

        public bool CheckUserValidation(string username,string password)
        {
            List<Users> query = (from a in db.Users
                                 where a.Username == (username) && a.Password == (password)
                                 select a).ToList();
            bool result = query.Any();
            return result;
        }

        public List<Users> CheckUserExists(string username)
        {
            List<Users> query = (from a in db.Users
                                 where a.Username.Equals(username)
                                 orderby a.Username
                                 select a).ToList();

            return query ;
        }
        
        public List<Gender> getGender()
        {
            var items = (from a in db.Gender
                         orderby a.GenderID
                         select a).ToList();
            return items;
        }
    }
}
