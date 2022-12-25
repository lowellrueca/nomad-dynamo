using System.Collections.Generic;
using System.Linq;
using Revit.Elements;
using DB = Autodesk.Revit.DB;

namespace NomadCore.Repository
{
    public class FamilyRepository : ElementRepositoryBase, IElementRepository<Family>
    {
        public IEnumerable<Family> GetElementsByCategory(Category category)
        {
            return GetElementsByCategory(category, isEditable: true, isInPlace: false);
        }

        public IEnumerable<Family> GetInPlaceElementsByCategory(Category category)
        {
            return GetElementsByCategory(category, isEditable: false, isInPlace: true);
        }

        IEnumerable<Family> GetElementsByCategory(Category category, bool isEditable, bool isInPlace)
        {
            DB.Category category_ = _document.Settings.Categories.get_Item(category.Name);

            DB.ElementClassFilter elementClassFilter = new DB.ElementClassFilter(typeof(DB.Family));

            List<Family> families = _filteredElementCollector.WherePasses(elementClassFilter)
                .WhereElementIsNotElementType()
                .Cast<DB.Family>()
                .Where(f => f.IsInPlace.Equals(isInPlace) && 
                            f.IsEditable.Equals(isEditable) && 
                            f.FamilyCategoryId.Equals(category_.Id))
                .Select(f => ElementWrapper.Wrap(f, true))
                .ToList();

            families.Sort((x, y) => x.Name.CompareTo(y.Name));

            return families;
        }
    }
}
