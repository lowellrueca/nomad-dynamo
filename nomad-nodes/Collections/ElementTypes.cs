using System.Collections.Generic;
using Revit.Elements;
using NomadCore.Repository;

namespace Collections
{
    public static class ElementTypes
    {
        static FamilyTypeRepository _familyTypeRepository = new FamilyTypeRepository();
        public static IEnumerable<FamilyType> ElementTypesByCategory(Category category)
        {
            return _familyTypeRepository.GetElementsByCategory(category);
        }
        public static IEnumerable<FamilyType> ElementTypesOfFamily(Element family)
        {
            return _familyTypeRepository.GetFamilyTypesOfFamily(family);
        }
    }
}
