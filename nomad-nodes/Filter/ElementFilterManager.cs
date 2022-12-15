using System;
using System.Collections.Generic;
using System.Linq;
using Parameters;
using Revit.Elements;

namespace Filter
{
    static class ElementFilterManager
    {
        public static IEnumerable<Element>? FilterElementByParameterValue(
            IEnumerable<Element> elements,
            string parameterName,
            object parameterValue,
            ParameterOfType parameterOfType)
        {
            List<Element>? elements_ = new List<Element>();

            switch (parameterOfType)
            {
                case ParameterOfType.Length:
                    elements_ = FilterElementByDoubleParameterType(
                                elements, parameterName, Convert.ToDouble(parameterValue), ParameterOfType.Length)
                                .ToList();
                    break;

                case ParameterOfType.Area:
                    elements_ = FilterElementByDoubleParameterType(
                                elements, parameterName, Convert.ToDouble(parameterValue), ParameterOfType.Area)
                                .ToList();
                    break;

                case ParameterOfType.MassPerUnitLength:
                    elements_ = FilterElementByDoubleParameterType(
                                elements, parameterName, Convert.ToDouble(parameterValue), ParameterOfType.MassPerUnitLength)
                                .ToList();
                    break;

                case ParameterOfType.Number:
                    elements_ = FilterElementByDoubleParameterType(
                                elements, parameterName, Convert.ToDouble(parameterValue), ParameterOfType.Number)
                                .ToList();

                    break;

                case ParameterOfType.Integer:
                    elements_ = FilterElementByIntegerParameterType(
                                elements, parameterName, Convert.ToInt32(parameterValue), ParameterOfType.Integer)
                                .ToList();
                    break;

                case ParameterOfType.YesNo:
                    elements_ = FilterElementByIntegerParameterType(
                                elements, parameterName, Convert.ToInt32(parameterValue), ParameterOfType.YesNo)
                                .ToList();
                    break;

                case ParameterOfType.Text:
                    elements_ = FilterElementByStringParameterType(
                                elements, parameterName, Convert.ToString(parameterValue), ParameterOfType.Text)
                                .ToList();
                    break;

                default:
                    elements_ = null;
                    break;
            }

            return elements_;
        }

        public static IEnumerable<Element>? FilterElementByDoubleParameterType(
            IEnumerable<Element> elements,
            string parameterName,
            double parameterValue,
            ParameterOfType parameterOfType)
        {
            List<Element>? elements_ = new List<Element>();

            Action<IEnumerable<Element>, string, double, ParameterOfType> addElements = (elems, pn, pv, pt) =>
            {
                bool result = false;

                foreach (Element e in elems.ToList())
                {
                    result = pv == Convert.ToDouble(ParameterCollector.GetParameterValues(e, pn, pt));

                    if (result == true)
                    {
                        elements_.Add(e);
                    }
                }
            };

            switch (parameterOfType)
            {
                case ParameterOfType.Length:
                    addElements(elements, parameterName, parameterValue, ParameterOfType.Length);
                    break;
                case ParameterOfType.Area:
                    addElements(elements, parameterName, parameterValue, ParameterOfType.Area);
                    break;
                case ParameterOfType.MassPerUnitLength:
                    addElements(elements, parameterName, parameterValue, ParameterOfType.MassPerUnitLength);
                    break;
                case ParameterOfType.Number:
                    addElements(elements, parameterName, parameterValue, ParameterOfType.Number);
                    break;

                default:
                    elements_ = null;
                    break;
            }

            return elements_;
        }

        public static IEnumerable<Element>? FilterElementByIntegerParameterType(
            IEnumerable<Element> elements,
            string parameterName,
            int parameterValue,
            ParameterOfType parameterOfType)
        {
            List<Element>? elements_ = new List<Element>();

            Action<IEnumerable<Element>, string, int, ParameterOfType> addElements = (elems, pn, pv, pt) =>
            {
                bool result = false;

                foreach (Element e in elems.ToList())
                {
                    result = pv == Convert.ToInt32(ParameterCollector.GetParameterValues(e, pn, pt));

                    if (result == true)
                    {
                        elements_.Add(e);
                    }
                }

            };

            switch (parameterOfType)
            {
                case ParameterOfType.Integer:
                    addElements(elements, parameterName, parameterValue, ParameterOfType.Integer);
                    break;

                case ParameterOfType.YesNo:
                    addElements(elements, parameterName, parameterValue, ParameterOfType.YesNo);
                    break;

                default:
                    elements_ = null;
                    break;
            }

            return elements_;
        }

        public static IEnumerable<Element>? FilterElementByStringParameterType(
            IEnumerable<Element> elements,
            string parameterName,
            string parameterValue,
            ParameterOfType parameterOfType)
        {
            List<Element>? elements_ = new List<Element>();

            Action<IEnumerable<Element>, string, string, ParameterOfType> addElements = (elems, pn, pv, pt) =>
            {
                bool result = false;

                foreach (Element e in elems.ToList())
                {
                    result = Convert.ToString(ParameterCollector.GetParameterValues(e, pn, pt)).Contains(pv);

                    if (result == true)
                    {
                        elements_.Add(e);
                    }
                }
            };

            switch (parameterOfType)
            {
                case ParameterOfType.Text:
                    addElements(elements, parameterName, parameterValue, ParameterOfType.Text);
                    break;

                default:
                    elements_ = null;
                    break;
            }

            return elements_;
        }
    }
}
