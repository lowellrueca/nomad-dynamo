using System.Collections.Generic;
using Parameters;
using Revit.Elements;
using Sort.Internal;

namespace Sort
{
    public static class SortElements
    {
        public static IEnumerable<Element>? SortElementsByParameter(
            IEnumerable<Element> elements,
            DataOfType dataOfType,
            params string[] parameterNames)
        {
            return SortManager.SortElementsByParameter(elements, dataOfType, parameterNames);
        }
    }
}
