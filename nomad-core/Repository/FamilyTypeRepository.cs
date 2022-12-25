using System.Collections.Generic;
using System.Linq;
using Revit.Elements;
using DB = Autodesk.Revit.DB;

namespace NomadCore.Repository
{
    public class FamilyTypeRepository : ElementRepositoryBase, IElementRepository<FamilyType>
    {
        public IEnumerable<FamilyType> GetElementsByCategory(Category category)
        {
            _category = _document.Settings.Categories.get_Item(category.Name);

            DB.ElementClassFilter elementClassFilter = new DB.ElementClassFilter(typeof(DB.FamilySymbol));

            List<FamilyType> familyTypes =_filteredElementCollector.OfCategoryId(_category.Id)
                .WherePasses(elementClassFilter)
                .WhereElementIsElementType()
                .Cast<DB.FamilySymbol>()
                .Select(f => ElementWrapper.Wrap(f, true))
                .ToList();

            familyTypes.Sort((x, y) => x.Name.CompareTo(y.Name));

            return familyTypes;
        }
        public IEnumerable<FamilyType> GetFamilyTypesOfFamily(Element family)
        {
            var fam = (Family)family;
            var famTypes = fam.Types.ToList();
            famTypes.Sort((x, y) => x.Name.CompareTo(y.Name));

            foreach (var famType in famTypes)
            {
                yield return famType;
            }
        }
    }
}
