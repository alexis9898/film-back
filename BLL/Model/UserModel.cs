using System;

namespace BLL.Model
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string _token { get; set; }
        public DateTime _tokenExpirationDate { get; set; }
    }
}
