using System;

namespace activity_model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? Created_At { get; set; } = new DateTime();
        public DateTime? Updated_At { get; set; } = new DateTime();
    }
}