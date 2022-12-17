using System.Collections.Generic;
using System.Linq;
using Parameters;
using Parameters.Internal;
using Revit.Elements;

namespace Sort.Internal
{
    static class SortManager
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
                    elems_ = elements.OrderBy(e => ParameterManager.GetParameterValueByElementId(e, parameterNames[0]));
                    break;

                case DataOfType.Integer:
                    elems_ = elements.OrderBy(e => ParameterManager.GetParameterValueByInteger(e, parameterNames[0]));
                    break;

                default:
                    elems_ = elements.OrderBy(e => ParameterManager.GetParameterValueByText(e, parameterNames[0]));
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
                    elems = elements.OrderBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[0]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[1]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[2]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[3]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[4]));
                    break;

                case 4:
                    elems = elements.OrderBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[0]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[1]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[2]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[3]));
                    break;

                case 3:
                    elems = elements.OrderBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[0]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[1]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[2]));
                    break;

                case 2:
                    elems = elements.OrderBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[0]))
                                    .ThenBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[1]));
                    break;

                default:
                    elems = elements.OrderBy(e => ParameterManager.GetParameterValueByDouble(e, parameterNames[0]));
                    break;
            }
            return elems;
        }
    }
}
