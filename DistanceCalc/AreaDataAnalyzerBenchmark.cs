using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceCalc
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AreaDataAnalyzerBenchmark
    {
        public static List<string> filenames = new List<string>(6)
            {
                "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_data1.json",
                "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_data2.json",
                "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_data3.json",
                "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_data4.json",
                "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_data5.json",
                "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_data6.json"
            };
        public const double distanceRange = 1000;
        public const int numberOfThreads = 4;
        public static User user = new User("newuser", "newpassword", "newemail@gmail.com", 25.9, 68.0, 0);
        public AreaDataAnalyzer analyzer = new AreaDataAnalyzer(distanceRange, user);
        public ParallelAreaDataAnalyzer parAnalyzer = new ParallelAreaDataAnalyzer(distanceRange, user, numberOfThreads);
        public List<User> userList = Program.ReadFile(filenames[1]);
        public const string resultPath = "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_results.json";
        public const string parResultPath = "../../../../../../../data/Bačkaitis_Giedrius_IFF-7_5_results_par.json";
        public List<User> results1 = new List<User>();
        public List<User> results2 = new List<User>();

        [Benchmark]
        public void FilterByDistance()
        {
            results1 = analyzer.FilterByDistance(this.userList);
        }

        [Benchmark]
        public void FilterByDistanceParallel()
        {
            results2 = parAnalyzer.FilterByDistance(this.userList);
        }
        /// <summary>
        /// Prints results to file in json format
        /// </summary>
        public void PrintResults()
        {
            using (StreamWriter writer = new StreamWriter(resultPath))
            {
                writer.WriteLine("[");
                foreach (var user in results1)
                {
                    var json = JsonConvert.SerializeObject(user);
                    writer.WriteLine(json + ",");
                }
                writer.Write("]");
            }
            using (StreamWriter writer = new StreamWriter(resultPath))
            {
                writer.WriteLine("[");
                foreach (var user in results2)
                {
                    var json = JsonConvert.SerializeObject(user);
                    writer.WriteLine(json + ",");
                }
                writer.Write("]");
            }
        }
    }
}
