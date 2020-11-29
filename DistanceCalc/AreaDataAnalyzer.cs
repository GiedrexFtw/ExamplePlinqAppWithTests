using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DistanceCalc
{
    public class AreaDataAnalyzer : IAreaDataAnalyzer
    {
        private double _distanceRange;
        private User _user;
        public AreaDataAnalyzer(double distanceRange, User user)
        {
            _distanceRange = distanceRange;
            _user = user;
        }

        public List<User> FilterByDistance(List<User> userList)
        {
            /* LINQ query which calculates distance for each user to the specified new user,
            filters users by the distance, orders the selected new list by username and distance
            and encrypts filtered users password with AES 128-bit cypher*/
            var usersInRange = from user in userList
                               where user != null
                               let distance = CalculateDistance(user)
                               where distance <= _distanceRange
                               orderby user.Username, distance
                               select new User
                               {
                                   Username = user.Username,
                                   Password = EncryptToAes(user.Password),
                                   Email = user.Email,
                                   Latitude = user.Latitude,
                                   Longitude = user.Longitude,
                                   Distance = distance
                               };
            // setups the email checker and converts filtered users to list
            //so that i can use remove method
            EmailAddressAttribute emailChecker = new EmailAddressAttribute();
            var usersInRangeList = usersInRange.ToList();
            //Filters users once again, this time it checks if email is in valid format
            foreach (var user in usersInRange)
            {
                if (!emailChecker.IsValid(user.Email))
                {
                    usersInRangeList.Remove(user);
                }
            }

            return usersInRangeList;
        }

        private double CalculateDistance(User user)
        {
            // distance between latitudes and longitudes 
            double dLat = (Math.PI / 180) * (_user.Latitude - user.Latitude);
            double dLon = (Math.PI / 180) * (_user.Longitude - user.Longitude);

            // convert to radians 
            double lat1 = (Math.PI / 180) * (user.Latitude);
            double lat2 = (Math.PI / 180) * (_user.Latitude);

            // apply formulae 
            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Pow(Math.Sin(dLon / 2), 2) *
                       Math.Cos(lat1) * Math.Cos(lat2);
            double rad = 6371;
            double c = 2 * Math.Asin(Math.Sqrt(a));

            return rad * c;
        }
        /// <summary>
        /// Encryption to aes method
        /// </summary>
        /// <param name="text"> plaintext </param>
        /// <returns></returns>
        public string EncryptToAes(string text)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] encrypted = Encrypt(text, aes.Key, aes.IV);

                    return Encoding.Default.GetString(encrypted);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

            return string.Empty;
        }
        /// <summary>
        /// Helper method to encypher the text, by using key and vector
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private byte[] Encrypt(string text, byte[] key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(text);
                        encrypted = ms.ToArray();
                    }
                }
            }

            return encrypted;
        }
    }
}
