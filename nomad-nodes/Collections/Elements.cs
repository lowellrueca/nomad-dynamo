using System.Collections.Generic;
using Revit.Elements;
using NomadCore.Repository;

namespace Collections
{
    public static class Elements
    {
        static ElementInstanceRepository _elementInstanceRepository= new ElementInstanceRepository();
        public static IEnumerable<Element> ElementsByCategory(Category category)
        {
            return _elementInstanceRepository.GetElementsByCategory(category);
        }

        public static IEnumerable<Element> ElementsOfElementType(Element familyType)
        {
            return _elementInstanceRepository.ElementsOfElementType(familyType);
        }
    }
}
