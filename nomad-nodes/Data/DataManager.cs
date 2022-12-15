using System.Collections.Generic;
using System.Linq;

namespace Data
{
    static class DataManager
    {
        public static IEnumerable<string> NumerizeDataSets(IEnumerable<object> data)
        {
            for (int i = 0; i < data.ToList().Count; i++)
            {
                yield return $"{data.ToList()[i]}-{i + 1}";
            }
        }

        public static IEnumerable<string> JoinDataSets(
            string delimeter,
            params IEnumerable<object>[] dataset)
        {
            switch (dataset.Count())
            {
                case 3:
                    var dataSetOf3 = dataset[0].Map(dataset[1], dataset[2], (a, b, c) =>
                                                    $"{a}{delimeter}{b}{delimeter}{c}");

                    foreach (var d in dataSetOf3) { yield return $"{d}"; }
                    break;

                case 2:
                    var dataSetOf2 = dataset[0].Map(dataset[1], (a, b) => $"{a}{delimeter}{b}");
                    foreach (var d in dataSetOf2) { yield return $"{d}"; }
                    break;

                default:
                    break;
            }
        }
    }
}
