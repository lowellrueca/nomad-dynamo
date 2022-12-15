using System.Collections.Generic;
using Revit.Elements;

namespace Parameters
{
    public static class GetParameters
    {
        public static IEnumerable<string> GetParameterNames(
            Element element,
            ParameterOfType parameterOfType)
        {
            return ParameterCollector.GetParameterNames(element, parameterOfType);
        }

        public static object GetParameterValues(
            Element element,
            string parameterName,
            ParameterOfType parameterOfType)
        {
            return ParameterCollector.GetParameterValues(element, parameterName, parameterOfType);
        }
    }
}
