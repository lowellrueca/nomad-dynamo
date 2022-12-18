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
        public static ParameterData GetParameterData(ParameterData parameterData, Element element, string parameterName)
        {
            var parameter = element.InternalElement.LookupParameter(parameterName);
            return ParameterToParameterData(parameterData, parameter);
        }

        public static IEnumerable<ParameterData> GetParameterData(ParameterData parameterData, Element element, DataOfType dataOfType)
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

            Action<ParameterData, List<DB.Parameter>> addToParamDatas = (paramData, params_) =>
            {
                foreach(DB.Parameter p in params_)
                {
                    parameterDatas.Add(ParameterToParameterData(paramData, p));
                }
            };

            switch(dataOfType)
            {
                case DataOfType.Double:
                    getParametersByStorageType(element, DB.StorageType.Double);
                    addToParamDatas(parameterData, parameters);
                    break;

                case DataOfType.ElementId:
                    getParametersByStorageType(element, DB.StorageType.ElementId);
                    addToParamDatas(parameterData, parameters);
                    break;
                    
                case DataOfType.Integer:
                    getParametersByStorageType(element, DB.StorageType.Integer);
                    addToParamDatas(parameterData, parameters);
                    break;
                    
                default:
                    getParametersByStorageType(element, DB.StorageType.String);
                    addToParamDatas(parameterData, parameters);
                    break;
            }
            
            return parameterDatas.Where(p => p.Parameter != null);
        }

        public static ParameterData ParameterToParameterData(ParameterData parameterData, DB.Parameter parameter)
        {
            parameterData = new ParameterData 
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
