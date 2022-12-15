using System.Collections.Generic;
using Parameters;
using Revit.Elements;

namespace Sort
{
    public static class SortElements
    {
        public static IEnumerable<Element>? SortElementsByParameter(
            IEnumerable<Element> elements,
            ParameterOfType parameterOfType,
            params string[] parameterNames)
        {
            return SortElementManager.SortElementsByParameter(elements, parameterOfType, parameterNames);
        }
    }
}
