using System.Collections.Generic;
using Parameters;
using Revit.Elements;
using Filter.Internal;

namespace Filter
{
    public static class FilterElements
    {
        public static IEnumerable<Element>? ByParameterValue(
            IEnumerable<Element> elements,
            string parameterName,
            object parameterValue,
            DataOfType dataOfType)
        {
            return FilterManager.FilterElementByParameterValue(
                elements, parameterName, parameterValue, dataOfType);
        }
    }
}
