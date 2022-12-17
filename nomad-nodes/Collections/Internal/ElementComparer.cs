using System;
using System.Collections.Generic;
using Revit.Elements;

namespace Collections.Internal
{
    class ElementComparer : IEqualityComparer<Element>
    {
        public bool Equals(Element x, Element y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Id == y.Id;
        }

        public int GetHashCode(Element element)
        {
            if (Object.ReferenceEquals(element, null)) return 0;
            int elementId = element.Id.GetHashCode();
            return elementId;
        }
    }
}