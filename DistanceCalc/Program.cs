using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;

namespace DistanceCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            /*new AreaDataAnalyzer(1000, new User("newuser", "newpassword", "newemail@gmail.com", 25.9, 68.0, 0)).FilterByDistance(new List<User>()
            {
               new User(username:"mock1", password: "mock1", email: "mock1@gmail.com",latitude:25.9,longitude:68.0, distance:0),
               new User(username:"mock2", password: "mock2", email: "mock2@yahoo.com",latitude:29.1,longitude:60.1, distance:0),
               new User(username:"mock3", password: "mock3", email: "mock3@gmail.com",latitude:54.4,longitude:14, distance:0),
               new User(username:"mock4", password: "mock4", email: "mock4",latitude:27.0,longitude:68.8, distance:0),
               new User(username:"mock5", password: "mock5", email: "mock5@gmail",latitude:29.7,longitude:69.9, distance:0),
               null
            });*/
            // Run the Benchmark
            BenchmarkRunner.Run<AreaDataAnalyzerBenchmark>();
        }
        /// <summary>
        /// Reads file from specified path and converts to list
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<User> ReadFile(string filename)
        {
            string json = string.Empty;
            if (!File.Exists(filename))
            {
                return null;
            }
            using (StreamReader reader = new StreamReader(filename))
            {
                json = reader.ReadToEnd();
            }
            if (json != null)
            {
                return JsonConvert.DeserializeObject<List<User>>(json);
            }
            return new List<User>();
            
        }
    }
}
