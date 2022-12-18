using System.Collections.Generic;
using System.Linq;
using Data;
using Parameters.Internal;
using Revit.Elements;

namespace Collections.Internal
{
    static class SortHandler
    {
        public static IEnumerable<Element>? SortElementsByParameter(
            IEnumerable<Element> elements,
            DataOfType dataOfType,
            params string[] parameterNames)
        {
            IEnumerable<Element>? elems_ = new List<Element>();

            switch (dataOfType)
            {
                case DataOfType.Double:
                    elems_ = SortByDataTypeOfDouble(elements, parameterNames);
                    break;

                case DataOfType.ElementId:
                    elems_ = elements.OrderBy(e => ParameterLookupValueHandler.GetElementIdValue(e, parameterNames[0]));
                    break;

                case DataOfType.Integer:
                    elems_ = elements.OrderBy(e => ParameterLookupValueHandler.GetIntegerValue(e, parameterNames[0]));
                    break;

                default:
                    elems_ = elements.OrderBy(e => ParameterLookupValueHandler.GetStringValue(e, parameterNames[0]));
                    elems_ = null;
                    break;
            }

            return elems_;
        }

        public static IEnumerable<Element> SortByDataTypeOfDouble(IEnumerable<Element> elements, params string[] parameterNames)
        {
            IEnumerable<Element> elems = new List<Element>();

            switch (parameterNames.Length)
            {
                case 5:
                    elems = elements.OrderBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[0]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[1]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[2]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[3]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[4]));
                    break;

                case 4:
                    elems = elements.OrderBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[0]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[1]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[2]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[3]));
                    break;

                case 3:
                    elems = elements.OrderBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[0]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[1]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[2]));
                    break;

                case 2:
                    elems = elements.OrderBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[0]))
                                    .ThenBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[1]));
                    break;

                default:
                    elems = elements.OrderBy(e => ParameterLookupValueHandler.GetDoubleValue(e, parameterNames[0]));
                    break;
            }
            return elems;
        }
    }
}
