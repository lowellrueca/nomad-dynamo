using System;
using System.Collections.Generic;
using System.Linq;
using Parameters;
using Revit.Elements;

namespace Context
{
    public static class ElementContext
    {
        public static IEnumerable<Element> FilterSquareElements(
            IEnumerable<Element> elements,
            string parameterName1,
            string parameterName2,
            ShapeContextOption shapeContextOption)
        {
            Func<Element, string, double> convert = (el, pn) =>
            {
                return Convert.ToDouble(ParameterCollector.GetParameterValues(el, pn, Parameters.ParameterOfType.Length));
            };

            foreach (var e in elements.ToList())
            {
                double paramValue1 = convert(e, parameterName1);
                double paramValue2 = convert(e, parameterName2);
                bool isEqual = paramValue1 == paramValue2;

                if (isEqual == false && shapeContextOption.Equals(ShapeContextOption.SortRHS))
                {
                    yield return e;
                }

                if (isEqual == true && shapeContextOption.Equals(ShapeContextOption.SortHSS))
                {
                    yield return e;
                }
            }
        }
    }
}
