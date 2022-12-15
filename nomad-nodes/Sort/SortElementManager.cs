using System.Collections.Generic;
using System.Linq;
using Parameters;
using Revit.Elements;

namespace Sort
{
    static class SortElementManager
    {
        public static IEnumerable<Element>? SortElementsByParameter(
            IEnumerable<Element> elements,
            ParameterOfType parameterOfType,
            params string[] parameterNames)
        {
            IEnumerable<Element>? elems_ = new List<Element>();

            switch (parameterNames.Length)
            {
                case 5:
                    elems_ = elements.OrderBy(e => ParameterCollector.GetParameterValues(e, parameterNames[0], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[1], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[2], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[3], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[4], parameterOfType));
                    break;

                case 4:
                    elems_ = elements.OrderBy(e => ParameterCollector.GetParameterValues(e, parameterNames[0], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[1], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[2], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[3], parameterOfType));
                    break;

                case 3:
                    elems_ = elements.OrderBy(e => ParameterCollector.GetParameterValues(e, parameterNames[0], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[1], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[2], parameterOfType));
                    break;

                case 2:
                    elems_ = elements.OrderBy(e => ParameterCollector.GetParameterValues(e, parameterNames[0], parameterOfType))
                                    .ThenBy(e => ParameterCollector.GetParameterValues(e, parameterNames[1], parameterOfType));
                    break;

                case 1:
                    elems_ = elements.OrderBy(e => ParameterCollector.GetParameterValues(e, parameterNames[0], parameterOfType));
                    break;

                default:
                    elems_ = null;
                    break;
            }

            return elems_;
        }
    }
}
