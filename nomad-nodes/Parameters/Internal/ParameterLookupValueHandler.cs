using DB = Autodesk.Revit.DB;
using Revit.Elements;

namespace Parameters.Internal
{
    class ParameterLookupValueHandler
    {
        public static double GetDoubleValue(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return ParameterValueHandler.GetDoubleValue(parameter);
        }
        public static DB.ElementId GetElementIdValue(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return ParameterValueHandler.GetElementIdValue(parameter);
        }
        public static int GetIntegerValue(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return ParameterValueHandler.GetIntegerValue(parameter);
        }
        public static string? GetStringValue(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return ParameterValueHandler.GetStringValue(parameter);
        }
    }
}
