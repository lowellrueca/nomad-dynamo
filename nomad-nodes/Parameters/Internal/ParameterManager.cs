using System.Collections.Generic;
using DB = Autodesk.Revit.DB;
using Revit.Elements;
using System.Linq;
using System;
using Collections.Internal;

namespace Parameters.Internal
{
    static class ParameterManager
    {
        public static double GetParameterValueByDouble(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return GetValueOfDouble(parameter);
        }
        public static DB.ElementId GetParameterValueByElementId(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return GetValueOfElementId(parameter);
        }
        public static int GetParameterValueByInteger(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return GetValueOfInteger(parameter);
        }
        public static string? GetParameterValueByText(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return GetValueOfText(parameter);
        }
        public static IEnumerable<Element> GetElements(IEnumerable<IEnumerable<ParameterData>> parameterDatas)
        {
            IEnumerable<Element> elements = parameterDatas.SelectMany(x => x).Select(x => x.Parameter.Element.ToDSType(true));
            return elements.Distinct(new ElementComparer()).ToList();
        }
        public static DB.DisplayUnitType GetDisplayUnitType(DB.Parameter parameter)
        {
            return new DB.Units(DB.UnitSystem.Metric)
                .GetFormatOptions(parameter.Definition.UnitType).DisplayUnits;
        }
        public static double GetValueOfDouble(DB.Parameter parameter)
        {
            double value;

            Func<double, DB.DisplayUnitType, double> round = (val, dut) =>
            {
                return Math.Round(DB.UnitUtils.ConvertFromInternalUnits(val, dut), 2);
            };

            switch (parameter.Definition.ParameterType)
            {
                case DB.ParameterType.Length:
                    value = round(parameter.AsDouble(), DB.DisplayUnitType.DUT_MILLIMETERS);
                    break;

                case DB.ParameterType.Area:
                    value = round(parameter.AsDouble(), DB.DisplayUnitType.DUT_SQUARE_MILLIMETERS);
                    break;

                case DB.ParameterType.MassPerUnitLength:
                    value = round(parameter.AsDouble(), DB.DisplayUnitType.DUT_KILOGRAMS_MASS_PER_METER);
                    break;

                default:
                    value = parameter.AsDouble();
                    break;
            }
            return value;
        }
        public static DB.ElementId GetValueOfElementId(DB.Parameter parameter)
        {
            return parameter.AsElementId();
        }
        public static int GetValueOfInteger(DB.Parameter parameter)
        {
            return parameter.AsInteger();
        }
        public static string? GetValueOfText(DB.Parameter parameter)
        {
            string? result = string.Empty;

            switch (parameter.Definition.ParameterType)
            {
                case DB.ParameterType.Text:
                    result = parameter.AsString();
                    break;

                default:
                    result = parameter.AsValueString();
                    break;
            }

            return result;
        }
        public static ParameterData GetParameterData(
            ParameterData parameterData, Element element, string parameterName, DataOfType dataOfType)
        {
            var param_ = element.InternalElement.LookupParameter(parameterName);

            parameterData = new ParameterData
            {
                Element = param_.Element.ToDSType(true),
                Parameter = param_,
                ParameterName = param_.Definition.Name,
                ParameterType = param_.Definition.ParameterType,
            };

            switch (dataOfType)
            {
                case DataOfType.Double:
                    parameterData.Double = ParameterManager.GetParameterValueByDouble(
                        parameterData.Element, parameterData.ParameterName);
                    break;

                case DataOfType.ElementId:
                    parameterData.ElementId = ParameterManager.GetParameterValueByElementId(
                        parameterData.Element, parameterData.ParameterName);
                    break;

                case DataOfType.Integer:
                    parameterData.Integer = ParameterManager.GetParameterValueByInteger(
                        parameterData.Element, parameterData.ParameterName);
                    break;

                default:
                    parameterData.String = ParameterManager.GetParameterValueByText(
                        parameterData.Element, parameterData.ParameterName);
                    break;
            }
            return parameterData;
        }
        public static IEnumerable<ParameterData> GetParameterData(Element element, DataOfType dataOfType)
        {
            List<ParameterData> parameterDatas = new List<ParameterData>();

            Func<Element, DB.Parameter, DB.StorageType, ParameterData> getParamData = (el, param_, stoType) =>
            {
                ParameterData parameterData = new ParameterData();
                if (param_.StorageType.Equals(stoType))
                {
                    parameterData.Element = el;
                    parameterData.Parameter = param_;
                    parameterData.ParameterName = param_.Definition.Name;
                    parameterData.ParameterType = param_.Definition.ParameterType;
                }
                return parameterData;
            };

            switch (dataOfType)
            {
                case DataOfType.Double:
                    foreach (var p in element.InternalElement.GetOrderedParameters())
                    {
                        var paramData = getParamData(element, p, DB.StorageType.Double);
                        paramData.Double = ParameterManager.GetValueOfDouble(p);
                        parameterDatas.Add(paramData);
                    }
                    break;

                case DataOfType.ElementId:
                    foreach (var p in element.InternalElement.GetOrderedParameters())
                    {
                        var paramData = getParamData(element, p, DB.StorageType.ElementId);
                        paramData.ElementId = ParameterManager.GetValueOfElementId(p);
                        parameterDatas.Add(paramData);
                    }
                    break;

                case DataOfType.Integer:
                    foreach (var p in element.InternalElement.GetOrderedParameters())
                    {
                        var paramData = getParamData(element, p, DB.StorageType.Integer);
                        paramData.Integer = ParameterManager.GetValueOfInteger(p);
                        parameterDatas.Add(paramData);
                    }
                    break;

                default:
                    foreach (var p in element.InternalElement.GetOrderedParameters())
                    {
                        var paramData = getParamData(element, p, DB.StorageType.String);
                        paramData.String = ParameterManager.GetValueOfText(p);
                        parameterDatas.Add(paramData);
                    }
                    break;
            }

            return parameterDatas.Where(p => p.Parameter != null);
        }
    }
}
