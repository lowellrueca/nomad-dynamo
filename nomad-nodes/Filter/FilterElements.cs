using System.Collections.Generic;
using Parameters;
using Revit.Elements;

namespace Filter
{
    public static class FilterElements
    {
        public static IEnumerable<Element>? ByParameterValue(
            IEnumerable<Element> elements,
            string parameterName,
            object parameterValue,
            ParameterOfType parameterOfType)
        {
            return ElementFilterManager.FilterElementByParameterValue(
                elements, parameterName, parameterValue, parameterOfType);
        }
    }
}
