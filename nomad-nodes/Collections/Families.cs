using System.Collections.Generic;
using Revit.Elements;
using NomadCore.Repository;

namespace Collections
{
    public static class Families
    {
        static FamilyRepository _familyRepository = new FamilyRepository();

        public static IEnumerable<Family> FamiliesByCategory(Category category)
        {
            return _familyRepository.GetElementsByCategory(category);
        }

        public static IEnumerable<Family> InPlaceFamiliesByCategory(Category category)
        {
            return _familyRepository.GetInPlaceElementsByCategory(category);
        }
    }
}
