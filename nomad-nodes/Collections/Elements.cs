using System.Collections.Generic;
using System.Linq;
using Revit.Elements;
using Revit.Elements.InternalUtilities;

namespace Collections
{
    public static class Elements
    {
        public static IEnumerable<Element> ElementsByCategory(Category category)
        {
            return ElementQueries.OfCategory(category);
        }

        public static IEnumerable<Element> ElementsOfElementType(Element familyType)
        {
            var famType = (FamilyType)familyType;
            var elems = ElementQueries.OfCategory(famType.GetCategory);
            var elems_ = elems.Where(e => e.ElementType.Id.Equals(famType.Id)).ToList();

            foreach (var e in elems_)
            {
                yield return e;
            }
        }
    }
}
