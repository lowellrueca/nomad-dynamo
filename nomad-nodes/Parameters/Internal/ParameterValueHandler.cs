using System;
using DB = Autodesk.Revit.DB;

namespace Parameters.Internal
{
    class ParameterValueHandler
    {
        public static double GetDoubleValue(DB.Parameter parameter)
        {
            double value;

            Func<double, DB.DisplayUnitType, double> round = (val, dut) =>
            {
                return Math.Round(DB.UnitUtils.ConvertFromInternalUnits(val, dut), 2);
            };

            switch (parameter.Definition.ParameterType)
            {
                case DB.ParameterType.Length:
                    value = round(parameter.AsDouble(), DB.DisplayUnitType.DUT_MILLIMETERS);
                    break;

                case DB.ParameterType.Area:
                    value = round(parameter.AsDouble(), DB.DisplayUnitType.DUT_SQUARE_MILLIMETERS);
                    break;

                case DB.ParameterType.MassPerUnitLength:
                    value = round(parameter.AsDouble(), DB.DisplayUnitType.DUT_KILOGRAMS_MASS_PER_METER);
                    break;

                default:
                    value = parameter.AsDouble();
                    break;
            }
            return value;
        }
        public static DB.ElementId GetElementIdValue(DB.Parameter parameter)
        {
            return parameter.AsElementId();
        }
        public static int GetIntegerValue(DB.Parameter parameter)
        {
            return parameter.AsInteger();
        }
        public static string? GetStringValue(DB.Parameter parameter)
        {
            string? result = string.Empty;

            switch (parameter.Definition.ParameterType)
            {
                case DB.ParameterType.Text:
                    result = parameter.AsString();
                    break;

                default:
                    result = parameter.AsValueString();
                    break;
            }

            return result;
        }
    }
}