using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Revit.Elements;
using DB = Autodesk.Revit.DB;

namespace Parameters.Internal
{
    class ParameterDataHandler
    {
        public static ParameterData GetParameterData(Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return ParameterToParameterData(parameter);
        }

        public static IEnumerable<ParameterData> GetParameterData(Element element, DataOfType dataOfType)
        {
            List<ParameterData> parameterDatas = new List<ParameterData>();
            List<DB.Parameter> parameters = new List<DB.Parameter>();

            Action<Element, DB.StorageType> getParametersByStorageType = (elem, storageType) =>
            {
                foreach (var p in elem.InternalElement.GetOrderedParameters())
                {
                    if(p.StorageType.Equals(storageType))
                    {
                        parameters.Add(p);
                    }
                }
            };

            // this function is needed to be isolated in order to flatten the list
            // from GetOrderedParameters of element list.
            Action<List<DB.Parameter>> addToParamDatas = (params_) =>
            {
                foreach(DB.Parameter p in params_)
                {
                    parameterDatas.Add(ParameterToParameterData(p));
                }
            };

            switch(dataOfType)
            {
                case DataOfType.Double:
                    getParametersByStorageType(element, DB.StorageType.Double);
                    addToParamDatas(parameters);
                    break;

                case DataOfType.ElementId:
                    getParametersByStorageType(element, DB.StorageType.ElementId);
                    addToParamDatas(parameters);
                    break;
                    
                case DataOfType.Integer:
                    getParametersByStorageType(element, DB.StorageType.Integer);
                    addToParamDatas(parameters);
                    break;
                    
                default:
                    getParametersByStorageType(element, DB.StorageType.String);
                    addToParamDatas(parameters);
                    break;
            }
            
            return parameterDatas.Where(p => p.Parameter != null);
        }

        public static ParameterData ParameterToParameterData(DB.Parameter parameter)
        {
            ParameterData parameterData = new ParameterData 
            {
                Parameter = parameter,
                Element = parameter.Element.ToDSType(true),
                ParameterName = parameter.Definition.Name,
            };

            switch (parameter.StorageType)
            {
                case DB.StorageType.Double:
                    parameterData.Double = ParameterValueHandler.GetDoubleValue(parameter);
                    break;

                case DB.StorageType.ElementId:
                    parameterData.ElementId = ParameterValueHandler.GetElementIdValue(parameter);
                    break;

                case DB.StorageType.Integer:
                    parameterData.Integer = ParameterValueHandler.GetIntegerValue(parameter);
                    break;

                default:
                    parameterData.String = ParameterValueHandler.GetStringValue(parameter);
                    break;
            }

            return parameterData;
        }
    }
}
