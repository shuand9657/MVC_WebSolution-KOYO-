using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModelClass.DBModel
{
    public class ClientViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    public class Clients
    {
        EFDBModelEntities db = new EFDBModelEntities();

        public List<Users> UserList()
        {
            var item = (from a in db.Users
                        orderby a.Username
                        select a).ToList();
            return item;
        }

        public List<Users> getUser(string username, string password)
        {
            var item = from a in UserList()
                       where a.Username.Equals(username) && a.Password.Equals(password)
                       select a;
            return item.ToList();
        }
    }
}
