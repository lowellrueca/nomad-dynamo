using System.Collections.Generic;

namespace Data
{
    public static class Collections
    {
        public static IEnumerable<string> NumerizeDataSets(IEnumerable<object> data)
        {
            return DataManager.NumerizeDataSets(data);
        }

        public static IEnumerable<string> JoinDataSets(
            string delimeter,
            params IEnumerable<object>[] dataset)
        {
            return DataManager.JoinDataSets(delimeter, dataset);
        }
    }
}
