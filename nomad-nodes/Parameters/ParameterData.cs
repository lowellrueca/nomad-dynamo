using Revit.Elements;
using DB = Autodesk.Revit.DB;
using Parameters.Internal;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Parameters
{
    public class ParameterData
    {
        public Element? Element { get; set; }
        public DB.Parameter? Parameter { get; set; }
        public string? ParameterName { get; set; }
        public DB.ParameterType? ParameterType { get; set; }
        public double Double { get; set; }
        public DB.ElementId? ElementId { get; set; }
        public int Integer { get; set; }
        public string? String { get; set; }
        internal ParameterData() { }
        public static IEnumerable<Element> GetDistinctElements(IEnumerable<IEnumerable<ParameterData>> parameterDatas)
        {
            return ParameterElementHandler.GetElements(parameterDatas);
        }
        public static ParameterData ParameterDataOfParameter(
            Element element,
            string parameterName)
        {
            return ParameterDataHandler.GetParameterData(element, parameterName);
        }
        public static IEnumerable<ParameterData> GetParameterDataOfElement(
            Element element,
            DataOfType dataOfType)
        {
            foreach(var p in ParameterDataHandler.GetParameterData(element, dataOfType))
            {
                yield return p;
            }
        }
        public static IEnumerable<(string, DB.ParameterType, DB.DisplayUnitType, DB.StorageType)> GetParameterDefinition(
            IEnumerable<ParameterData> parameterDatas)
        {
            return parameterDatas.Select(x => (x.Parameter.Definition.Name,
                                                x.Parameter.Definition.ParameterType,
                                                ParameterUnitHandler.GetDisplayUnitType(x.Parameter),
                                                x.Parameter.StorageType));
        }
    }
}
