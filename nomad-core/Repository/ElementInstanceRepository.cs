using System.Collections.Generic;
using System.Linq;
using Revit.Elements;
using Revit.Elements.InternalUtilities;
using DB = Autodesk.Revit.DB;

namespace NomadCore.Repository
{
    public class ElementInstanceRepository : ElementRepositoryBase, IElementRepository<Element>
    {
        public IEnumerable<Element> GetElementsByCategory(Category category)
        {
            _category = _document.Settings.Categories.get_Item(category.Name);

            DB.ElementClassFilter elementClassFilter = new DB.ElementClassFilter(typeof(DB.Element));

            List<Element> elements =_filteredElementCollector.OfCategoryId(_category.Id)
                .WherePasses(elementClassFilter)
                .WhereElementIsNotElementType()
                .Cast<DB.Element>()
                .Select(f => f.ToDSType(true))
                .ToList();

            elements.Sort((x, y) => x.Name.CompareTo(y.Name));

            return elements;
        }
        public IEnumerable<Element> ElementsOfElementType(Element familyType)
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
