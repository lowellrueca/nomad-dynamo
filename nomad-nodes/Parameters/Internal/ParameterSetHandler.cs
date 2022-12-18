using System;
using System.Collections.Generic;
using System.Linq;
using DB = Autodesk.Revit.DB;
using Revit.Elements;
using Data;

namespace Parameters.Internal
{
    static class ParameterSetHandler
    {
        public static void SetParameterValue(
            IEnumerable<Element> elements,
            string parameterName,
            object data,
            DataOfType dataOfType)
        {
            Func<IEnumerable<Element>, string, List<DB.Parameter>> getParam = (elems, paramName) =>
            {
                return elems.Select(e => e.InternalElement.LookupParameter(parameterName)).ToList();
            };

            switch (dataOfType)
            {
                case DataOfType.Double:
                    var dataOfLength = (double)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfLength));
                    break;

                case DataOfType.ElementId:
                    var dataOfElemId = (DB.ElementId)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfElemId));
                    break;

                case DataOfType.Integer:
                    var dataOfInt = (int)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfInt));
                    break;

                default:
                    var stringdata = (string)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(stringdata));
                    break;
            }
        }
        public static void SetParameterValues(
            IEnumerable<Element> elements,
            string parameterName,
            IEnumerable<object> data,
            DataOfType dataOfType)
        {
            Func<IEnumerable<object>, IEnumerable<double>> convertToDoubles = (data_) => { return data_.ToList().Select(d => (double)d); };
            Func<IEnumerable<object>, IEnumerable<DB.ElementId>> convertToElemIds = (data_) => { return data_.ToList().Select(d => (DB.ElementId)d); };
            Func<IEnumerable<object>, IEnumerable<int>> convertToInts = (data_) => { return data_.ToList().Select(d => (int)d); };
            Func<IEnumerable<object>, IEnumerable<string>> convertToStrings = (data_) => { return data_.ToList().Select(d => (string)d); };

            switch (dataOfType)
            {
                case DataOfType.Double:
                    var dataOfLengths = convertToDoubles(data);
                    IEnumerable<(DB.Parameter, double)> dataSetLengths = elements.Zip(dataOfLengths, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataSetLengths) { p.Set(d); }
                    break;

                case DataOfType.ElementId:
                    var dataOfNumber = convertToElemIds(data);
                    IEnumerable<(DB.Parameter, DB.ElementId)> dataOfNumbers = elements.Zip(dataOfNumber, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataOfNumbers) { p.Set(d); }
                    break;

                case DataOfType.Integer:
                    var dataOfInt = convertToInts(data);
                    IEnumerable<(DB.Parameter, int)> dataOfInts = elements.Zip(dataOfInt, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataOfInts) { p.Set(d); }
                    break;

                default:
                    var defaultData = convertToStrings(data);
                    IEnumerable<(DB.Parameter, string)> defaultDatas = elements.Zip(defaultData, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in defaultDatas) { p.Set(d); }
                    break;
            }
        }
    }
}
