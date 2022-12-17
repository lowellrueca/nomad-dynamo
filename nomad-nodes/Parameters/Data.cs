using System.Collections.Generic;
using Parameters.Internal;
using Revit.Elements;
using DB = Autodesk.Revit.DB;
using System.Linq;

namespace Parameters
{
    public static class Data
    {
        public static IEnumerable<Element> GetElements(IEnumerable<IEnumerable<ParameterData>> parameterDatas)
        {
            return ParameterManager.GetElements(parameterDatas);
        }
        public static IEnumerable<ParameterData> GetParameterDataOfParameterName(
            ParameterData parameterData,
            IEnumerable<Element> elements,
            string parameterName,
            DataOfType dataOfType)
        {
            foreach (var e in elements)
            {
                yield return ParameterManager.GetParameterData(parameterData, e, parameterName, dataOfType);
            }
        }
        public static IEnumerable<ParameterData> GetParameterDataOfElements(
            IEnumerable<Element> elements, DataOfType dataOfType)
        {
            IEnumerable<ParameterData> parameterDatas = new List<ParameterData>();
            foreach (var e in elements)
            {
                parameterDatas = ParameterManager.GetParameterData(e, dataOfType);
            }
            return parameterDatas;
        }

        public static IEnumerable<(string, DB.ParameterType, DB.DisplayUnitType, DB.StorageType)> GetParameterDefinition(
            IEnumerable<ParameterData> parameterDatas)
        {
            return parameterDatas.Select(x => (x.Parameter.Definition.Name,
                                                x.Parameter.Definition.ParameterType,
                                                ParameterManager.GetDisplayUnitType(x.Parameter),
                                                x.Parameter.StorageType));
        }
    }
}
