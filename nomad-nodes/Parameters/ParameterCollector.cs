using System.Collections.Generic;
using DB = Autodesk.Revit.DB;
using Revit.Elements;
using System.Linq;
using System;

namespace Parameters
{
    static class ParameterCollector
    {
        public static IEnumerable<DB.Parameter> GetParameterByType(
            Element element,
            ParameterOfType parameterOfType)
        {
            List<DB.Parameter> parameters = new List<DB.Parameter>();
            var getOrderedParameters = element.InternalElement.GetOrderedParameters();

            Action<DB.ParameterType> addParameters = (pt) =>
            {
                getOrderedParameters.Where(p => p.Definition.ParameterType.Equals(pt)).ToList()
                        .ForEach(p => parameters.Add(p));
            };

            switch (parameterOfType)
            {
                case ParameterOfType.Length:
                    addParameters(DB.ParameterType.Length);
                    break;

                case ParameterOfType.Area:
                    addParameters(DB.ParameterType.Area);
                    break;

                case ParameterOfType.MassPerUnitLength:
                    addParameters(DB.ParameterType.MassPerUnitLength);
                    break;

                case ParameterOfType.Number:
                    addParameters(DB.ParameterType.Number);
                    break;

                case ParameterOfType.Integer:
                    addParameters(DB.ParameterType.Integer);
                    break;

                case ParameterOfType.YesNo:
                    addParameters(DB.ParameterType.YesNo);
                    break;

                default:
                    getOrderedParameters.Where(p => p.Definition.ParameterType.Equals(DB.ParameterType.Text))
                            .ToList().ForEach(p => parameters.Add(p));

                    break;
            }

            return parameters;
        }

        public static IEnumerable<string> GetParameterNames(
            Element element,
            ParameterOfType parameterOfType)
        {
            var params_ = GetParameterByType(element, parameterOfType).ToList();
            foreach (var p in params_)
            {
                yield return p.Definition.Name;
            }
        }

        public static object GetParameterValues(
            Element element,
            string parameterName,
            ParameterOfType parameterOfType)
        {
            object? res = null;
            var param = element.InternalElement.LookupParameter(parameterName);

            Func<double, DB.DisplayUnitType, double> round = (val, dut) =>
            {
                return Math.Round(DB.UnitUtils.ConvertFromInternalUnits(val, dut), 2);
            };

            switch (parameterOfType)
            {
                case ParameterOfType.Length:
                    res = round(param.AsDouble(), DB.DisplayUnitType.DUT_MILLIMETERS);
                    break;

                case ParameterOfType.Area:
                    res = round(param.AsDouble(), DB.DisplayUnitType.DUT_SQUARE_MILLIMETERS);
                    break;

                case ParameterOfType.MassPerUnitLength:
                    res = round(param.AsDouble(), DB.DisplayUnitType.DUT_KILOGRAMS_MASS_PER_METER);
                    break;

                case ParameterOfType.Number:
                    res = param.AsDouble();
                    break;

                case ParameterOfType.Integer:
                    res = param.AsInteger();
                    break;

                case ParameterOfType.YesNo:
                    res = param.AsInteger();
                    break;

                case ParameterOfType.Text:
                    res = param.AsString();
                    break;

                default:
                    res = param.AsValueString();
                    break;
            }

            return res;
        }
    }
}
