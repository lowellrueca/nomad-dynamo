using System.Collections.Generic;
using DB = Autodesk.Revit.DB;
using Revit.Elements;
using RevitServices.Persistence;
using System.Linq;
using Collections.Internal;

namespace Collections
{
    public static class ElementTypes
    {
        public static IEnumerable<FamilyType> ElementTypesByCategory(Category category)
        {
            DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
            DB.Category cat = doc.Settings.Categories.get_Item(category.Name);

            var ecf = new DB.ElementClassFilter(typeof(DB.ElementType));
            List<DB.FamilySymbol> famSymbols = new DB.FilteredElementCollector(doc)
                    .OfCategoryId(cat.Id)
                    .WherePasses(ecf)
                    .WhereElementIsElementType()
                    .Cast<DB.FamilySymbol>()
                    .Where(f => f.Family.IsInPlace.Equals(false) && f.Family.IsEditable.Equals(true))
                    .ToList();

            famSymbols.Sort(new FamilySymbolComparer());

            foreach (var famsymbol in famSymbols)
            {
                yield return (FamilyType)famsymbol.ToDSType(true);
            }
        }
        public static IEnumerable<FamilyType> ElementTypesOfFamily(Element family)
        {
            var fam = (Family)family;
            var famTypes = fam.Types.ToList();
            famTypes.Sort(new FamilyTypeComparer());

            foreach (var famType in famTypes)
            {
                yield return famType;
            }
        }
    }
}
