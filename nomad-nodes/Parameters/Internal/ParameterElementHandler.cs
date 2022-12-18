using System.Collections.Generic;
using Revit.Elements;
using System.Linq;
using Collections.Internal;

namespace Parameters.Internal
{
    class ParameterElementHandler
    {
        public static IEnumerable<Element> GetElements(IEnumerable<IEnumerable<ParameterData>> parameterDatas)
        {
            IEnumerable<Element> elements = parameterDatas.SelectMany(x => x).Select(x => x.Parameter.Element.ToDSType(true));
            return elements.Distinct(new ElementComparer()).ToList();
        }
    }
}
