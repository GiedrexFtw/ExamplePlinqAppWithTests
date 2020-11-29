using System.Collections.Generic;

namespace DistanceCalc
{
    interface IAreaDataAnalyzer
    {
        List<User> FilterByDistance(List<User> userList);
        string EncryptToAes(string text);
    }
}