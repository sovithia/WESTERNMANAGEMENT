using System;
namespace WesternManagementApp
{
    public class USER
    {
        public USER() { }

        public string id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }

        public string modules { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string role { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        

    }

}

