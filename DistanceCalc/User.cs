using System;
using System.Collections.Generic;
using System.Text;

namespace DistanceCalc
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }

        public User(string username, string password, string email,
            double latitude, double longitude, double distance)
        {
            Username = username;
            Password = password;
            Email = email;
            Latitude = latitude;
            Longitude = longitude;
            Distance = distance;
        }
        public User() { }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return this.Username == user.Username && this.Email == user.Email;
        }
    }
}
