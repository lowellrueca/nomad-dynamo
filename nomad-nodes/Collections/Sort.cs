using System.Collections.Generic;
using Data;
using Revit.Elements;
using Collections.Internal;

namespace Collections
{
    public static class Sort
    {
        public static IEnumerable<Element>? SortElementsByParameter(
            IEnumerable<Element> elements,
            DataOfType dataOfType,
            params string[] parameterNames)
        {
            return SortHandler.SortElementsByParameter(elements, dataOfType, parameterNames);
        }
    }
}
