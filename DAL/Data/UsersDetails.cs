using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    internal class UsersDetails
    {
        public int Id { get; set; }
        public string appUserId { get; set; }
        public AppUser appUser { get; set; }
        public IEnumerable<int> filmsIdLikes { get; set; }

    }
}
