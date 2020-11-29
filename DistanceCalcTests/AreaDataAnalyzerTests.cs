using Xunit;
using DistanceCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceCalc.Tests
{
    

    public class AreaDataAnalyzerTests
    {
        private AreaDataAnalyzerBenchmark benchmark = new AreaDataAnalyzerBenchmark();
        
        //Setting up mock data
        public AreaDataAnalyzerTests()
        {
            benchmark.userList = new List<User>()
            {
               new User(username:"mock1", password: "mock1", email: "mock1@gmail.com",
               latitude:25.9,longitude:68.0, distance:0),
               new User(username:"mock2", password: "mock2", email: "mock2@yahoo.com",
               latitude:29.1,longitude:60.1, distance:0),
               new User(username:"mock3", password: "mock3", email: "mock3@gmail.com",
               latitude:54.4,longitude:14, distance:0),
               new User(username:"mock4", password: "mock4", email: "mock4",
               latitude:27.0,longitude:68.8, distance:0),
               new User(username:"mock5", password: "mock5", email: "mock5@",
               latitude:29.7,longitude:69.9, distance:0),
               null
            };
            
        }
        [Fact]
        public void FilterByDistanceNormalTest()
        {
            benchmark.FilterByDistance();
            var list = benchmark.results1;
            Assert.NotEmpty(list);
            benchmark.userList.RemoveRange(2, 4);
            Assert.Equal(benchmark.userList, list);
        }

        [Fact]
        public void FilterByDistanceParallelTest()
        {
            benchmark.FilterByDistanceParallel();
            var list = benchmark.results2;
            Assert.NotEmpty(list);
            benchmark.userList.RemoveRange(2, 4);
            Assert.Equal(benchmark.userList, list);
        }
    }
}