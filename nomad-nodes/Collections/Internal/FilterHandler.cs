using System.Collections.Generic;
using System.Linq;
using DB = Autodesk.Revit.DB;
using Revit.Elements;
using Parameters.Internal;
using Data;

namespace Collections.Internal
{
    static class FilterHandler
    {
        public static IEnumerable<Element>? FilterElementByParameterValue(
            IEnumerable<Element> elements,
            string parameterName,
            object parameterValue,
            DataOfType dataOfType)
        {
            List<Element> elements_ = new List<Element>();

            switch (dataOfType)
            {
                case DataOfType.Double:
                    var double_ = (double)parameterValue;
                    elements_ = FilterElementByDataTypeOfDouble(elements, parameterName, double_).ToList();
                    break;

                case DataOfType.ElementId:
                    var elemId = (DB.ElementId)parameterValue;
                    elements_ = FilterElementByDataTypeOfElementId(elements, parameterName, elemId).ToList();
                    break;

                case DataOfType.Integer:
                    var int_ = (int)parameterValue;
                    elements_ = FilterElementByDataTypeOfInteger(elements, parameterName, int_).ToList();
                    break;

                case DataOfType.String:
                    var text = (string)parameterValue;
                    elements_ = FilterElementByDataTypeOfString(elements, parameterName, text).ToList();
                    break;

                default:
                    break;
            }
            return elements_;
        }

        public static IEnumerable<Element> FilterElementByDataTypeOfDouble(
            IEnumerable<Element> elements,
            string parameterName,
            double parameterValue)
        {
            foreach (var e in elements)
            {
                var paramValue = ParameterLookupValueHandler.GetDoubleValue(e, parameterName);
                if (paramValue == parameterValue)
                {
                    yield return e;
                }
            }
        }

        public static IEnumerable<Element> FilterElementByDataTypeOfElementId(
            IEnumerable<Element> elements,
            string parameterName,
            DB.ElementId parameterValue)
        {
            foreach (var e in elements)
            {
                var paramValue = ParameterLookupValueHandler.GetElementIdValue(e, parameterName);
                if (paramValue == parameterValue)
                {
                    yield return e;
                }
            }
        }
        public static IEnumerable<Element> FilterElementByDataTypeOfInteger(
            IEnumerable<Element> elements,
            string parameterName,
            int parameterValue)
        {
            foreach (var e in elements)
            {
                var paramValue = ParameterLookupValueHandler.GetIntegerValue(e, parameterName);
                if (paramValue == parameterValue)
                {
                    yield return e;
                }
            }
        }
        public static IEnumerable<Element> FilterElementByDataTypeOfString(
            IEnumerable<Element> elements,
            string parameterName,
            string parameterValue)
        {
            foreach (var e in elements)
            {
                var paramValue = ParameterLookupValueHandler.GetStringValue(e, parameterName);
                if (paramValue.Contains(parameterValue))
                {
                    yield return e;
                }
            }
        }

        public static IEnumerable<Element> FilterElementsWithEqualXYParameter(
            IEnumerable<Element> elements,
            string parameterNameX,
            string parameterNameY)
        {
            foreach (var e in elements)
            {
                var paramX = e.InternalElement.LookupParameter(parameterNameX);
                var paramY = e.InternalElement.LookupParameter(parameterNameY);
                var storageType = paramX.StorageType == paramY.StorageType ? paramX.StorageType : DB.StorageType.None;

                switch (storageType)
                {
                    case DB.StorageType.Double:
                        if (ParameterValueHandler.GetDoubleValue(paramX).Equals(ParameterValueHandler.GetDoubleValue(paramY)))
                        { yield return e; }
                        break;

                    case DB.StorageType.ElementId:
                        if (ParameterValueHandler.GetElementIdValue(paramX).Equals(ParameterValueHandler.GetElementIdValue(paramY)))
                        { yield return e; }
                        break;

                    case DB.StorageType.Integer:
                        if (ParameterValueHandler.GetIntegerValue(paramX).Equals(ParameterValueHandler.GetIntegerValue(paramY)))
                        { yield return e; }
                        break;

                    case DB.StorageType.String:
                        if (ParameterValueHandler.GetStringValue(paramX).Equals(ParameterValueHandler.GetStringValue(paramY)))
                        { yield return e; }
                        break;

                    default:
                        break;
                }
            }
        }
        public static IEnumerable<Element> FilterElementsWithUnEqualXYParameter(
            IEnumerable<Element> elements,
            string parameterNameX,
            string parameterNameY)
        {
            foreach (var e in elements)
            {
                var paramX = e.InternalElement.LookupParameter(parameterNameX);
                var paramY = e.InternalElement.LookupParameter(parameterNameY);
                var storageType = paramX.StorageType == paramY.StorageType ? paramX.StorageType : DB.StorageType.None;

                switch (storageType)
                {
                    case DB.StorageType.Double:
                        if (!ParameterValueHandler.GetDoubleValue(paramX).Equals(ParameterValueHandler.GetDoubleValue(paramY)))
                        { yield return e; }
                        break;

                    case DB.StorageType.ElementId:
                        if (!ParameterValueHandler.GetElementIdValue(paramX).Equals(ParameterValueHandler.GetElementIdValue(paramY)))
                        { yield return e; }
                        break;

                    case DB.StorageType.Integer:
                        if (!ParameterValueHandler.GetIntegerValue(paramX).Equals(ParameterValueHandler.GetIntegerValue(paramY)))
                        { yield return e; }
                        break;

                    case DB.StorageType.String:
                        if (!ParameterValueHandler.GetStringValue(paramX).Equals(ParameterValueHandler.GetStringValue(paramY)))
                        { yield return e; }
                        break;

                    default:
                        break;
                }
            }
        }
        public static IEnumerable<Element> FilterElementsByStringQuery(IEnumerable<Element> elements, string parameterName, string parameterValue, StringQuery stringQuery)
        {
            switch(stringQuery)
            {
                case StringQuery.Contains:
                    foreach(var e in elements)
                    {
                        var paramValue = ParameterLookupValueHandler.GetStringValue(e, parameterName);
                        if(paramValue.Contains(parameterValue))
                        {
                            yield return e;
                        }
                    }
                    break;

                case StringQuery.Equals:
                    foreach(var e in elements)
                    {
                        var paramValue = ParameterLookupValueHandler.GetStringValue(e, parameterName);
                        if(paramValue.Equals(parameterValue))
                        {
                            yield return e;
                        }
                    }
                    break;

                case StringQuery.StartsWith:
                    foreach(var e in elements)
                    {
                        var paramValue = ParameterLookupValueHandler.GetStringValue(e, parameterName);
                        if(paramValue.StartsWith(parameterValue))
                        {
                            yield return e;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
