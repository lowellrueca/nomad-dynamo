using Autodesk.Revit.DB;
using DM = RevitServices.Persistence.DocumentManager;

namespace NomadCore.Repository
{
    public class ElementRepositoryBase
    {
        public Category? _category;
        public Document _document;
        public FilteredElementCollector _filteredElementCollector;
        public ElementRepositoryBase()
        {
            _document = DM.Instance.CurrentDBDocument;
            _filteredElementCollector = new FilteredElementCollector(_document);
        }
    }
}
