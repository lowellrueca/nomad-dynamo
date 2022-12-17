using System.Collections.Generic;
using System.Linq;
using Parameters;
using DB = Autodesk.Revit.DB;
using Revit.Elements;
using Parameters.Internal;

namespace Filter.Internal
{
    static class FilterManager
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

                default:
                    var text = (string)parameterValue;
                    elements_ = FilterElementByDataTypeOfText(elements, parameterName, text).ToList();
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
                var param_ = e.InternalElement.LookupParameter(parameterName);
                var double_ = ParameterManager.GetValueOfDouble(param_);
                if (parameterValue == double_)
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
                var param_ = e.InternalElement.LookupParameter(parameterName);
                var elementId = ParameterManager.GetValueOfElementId(param_);
                if (parameterValue == elementId)
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
                var param_ = e.InternalElement.LookupParameter(parameterName);
                var int_ = ParameterManager.GetValueOfInteger(param_);
                if (parameterValue == int_)
                {
                    yield return e;
                }
            }
        }
        public static IEnumerable<Element> FilterElementByDataTypeOfText(
            IEnumerable<Element> elements,
            string parameterName,
            string parameterValue)
        {
            foreach (var e in elements)
            {
                var param_ = e.InternalElement.LookupParameter(parameterName);
                var text = ParameterManager.GetValueOfText(param_);
                if (parameterValue.Contains(text))
                {
                    yield return e;
                }
            }
        }
    }
}
