using System;
using System.Collections.Generic;
using System.Linq;
using DB = Autodesk.Revit.DB;
using Revit.Elements;

namespace Parameters
{
    static class ParameterUpdater
    {
        public static void SetParameterValue(
            IEnumerable<Element> elements,
            string parameterName,
            object data,
            ParameterOfType parameterOfType)
        {
            Func<IEnumerable<Element>, string, List<DB.Parameter>> getParam = (elems, paramName) =>
            {
                return elems.Select(e => e.InternalElement.LookupParameter(parameterName)).ToList();
            };

            switch (parameterOfType)
            {
                case ParameterOfType.Length:
                    var dataOfLength = (double)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfLength));
                    break;

                case ParameterOfType.Area:
                    var dataOfArea = (double)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfArea));
                    break;

                case ParameterOfType.MassPerUnitLength:
                    var dataOfMassPerUnitLength = (double)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfMassPerUnitLength));
                    break;

                case ParameterOfType.Number:
                    var dataOfNumber = (double)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfNumber));
                    break;

                case ParameterOfType.Integer:
                    var dataOfInt = (int)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfInt));
                    break;

                case ParameterOfType.YesNo:
                    var dataOfYesNo = (int)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfYesNo));
                    break;

                case ParameterOfType.Text:
                    var dataOfText = (string)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(dataOfText));
                    break;

                default:
                    var defaultData = (string)data;
                    getParam(elements, parameterName).ForEach(p => p.Set(defaultData));
                    break;
            }
        }
        public static void SetParameterValues(
            IEnumerable<Element> elements,
            string parameterName,
            IEnumerable<object> data,
            ParameterOfType parameterOfType)
        {
            Func<IEnumerable<object>, IEnumerable<double>> convertToDoubles = (data_) => { return data_.ToList().Select(d => (double)d); };
            Func<IEnumerable<object>, IEnumerable<int>> convertToInts = (data_) => { return data_.ToList().Select(d => (int)d); };
            Func<IEnumerable<object>, IEnumerable<string>> convertToStrings = (data_) => { return data_.ToList().Select(d => (string)d); };

            switch (parameterOfType)
            {
                case ParameterOfType.Length:
                    var dataOfLengths = convertToDoubles(data);
                    IEnumerable<(DB.Parameter, double)> dataSetLengths = elements.Zip(dataOfLengths, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataSetLengths) { p.Set(d); }
                    break;

                case ParameterOfType.Area:
                    var dataOfArea = convertToDoubles(data);
                    IEnumerable<(DB.Parameter, double)> dataSetAreas = elements.Zip(dataOfArea, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataSetAreas) { p.Set(d); }
                    break;

                case ParameterOfType.MassPerUnitLength:
                    var dataOfMassPerUnitLength = convertToDoubles(data);
                    IEnumerable<(DB.Parameter, double)> dataMassPerUnitLengths = elements.Zip(dataOfMassPerUnitLength, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataMassPerUnitLengths) { p.Set(d); }
                    break;

                case ParameterOfType.Number:
                    var dataOfNumber = convertToDoubles(data);
                    IEnumerable<(DB.Parameter, double)> dataOfNumbers = elements.Zip(dataOfNumber, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataOfNumbers) { p.Set(d); }
                    break;

                case ParameterOfType.Integer:
                    var dataOfInt = convertToInts(data);
                    IEnumerable<(DB.Parameter, int)> dataOfInts = elements.Zip(dataOfInt, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataOfInts) { p.Set(d); }
                    break;

                case ParameterOfType.YesNo:
                    var dataOfYesNo = convertToInts(data);
                    IEnumerable<(DB.Parameter, int)> dataOfYesNos = elements.Zip(dataOfYesNo, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataOfYesNos) { p.Set(d); }
                    break;

                case ParameterOfType.Text:
                    var dataOfText = convertToStrings(data);
                    IEnumerable<(DB.Parameter, string)> dataOfTexts = elements.Zip(dataOfText, (e, d) =>
                                        (e.InternalElement.LookupParameter(parameterName), d)).ToList();

                    foreach (var (p, d) in dataOfTexts) { p.Set(d); }
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
