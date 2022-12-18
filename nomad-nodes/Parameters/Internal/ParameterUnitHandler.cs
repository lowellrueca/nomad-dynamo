using DB = Autodesk.Revit.DB;

namespace Parameters.Internal
{
    class ParameterUnitHandler
    {
        public static DB.DisplayUnitType GetDisplayUnitType(DB.Parameter parameter)
        {
            return new DB.Units(DB.UnitSystem.Metric)
                .GetFormatOptions(parameter.Definition.UnitType).DisplayUnits;
        }
    }
}
