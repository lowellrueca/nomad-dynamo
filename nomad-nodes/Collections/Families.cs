using System.Collections.Generic;
using DB = Autodesk.Revit.DB;
using Revit.Elements;
using RevitServices.Persistence;
using System.Linq;

namespace Collections
{
    public static class Families
    {
        public static IEnumerable<Family> FamiliesByCategory(Category category)
        {
            DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
            DB.Category cat = doc.Settings.Categories.get_Item(category.Name);

            var ecf = new DB.ElementClassFilter(typeof(DB.Family));
            List<DB.Family> fams = new DB.FilteredElementCollector(doc)
                    .WherePasses(ecf)
                    .WhereElementIsNotElementType()
                    .Cast<DB.Family>()
                    .Where(f => f.IsInPlace.Equals(false) && f.FamilyCategoryId.Equals(cat.Id))
                    .ToList();

            fams.Sort((x, y) => x.Name.CompareTo(y.Name));

            foreach (var fam in fams)
            {
                yield return (Family)fam.ToDSType(true);
            }
        }

        public static IEnumerable<Family> InPlaceFamiliesByCategory(Category category)
        {
            DB.Document doc = DocumentManager.Instance.CurrentDBDocument;
            DB.Category cat = doc.Settings.Categories.get_Item(category.Name);

            var ecf = new DB.ElementClassFilter(typeof(DB.Family));
            List<DB.Family> fams = new DB.FilteredElementCollector(doc)
                    .WherePasses(ecf)
                    .WhereElementIsNotElementType()
                    .Cast<DB.Family>()
                    .Where(f => f.IsInPlace.Equals(true) && f.FamilyCategoryId.Equals(cat.Id))
                    .ToList();

            foreach (var fam in fams)
            {
                yield return (Family)fam.ToDSType(true);
            }
        }
    }
}
