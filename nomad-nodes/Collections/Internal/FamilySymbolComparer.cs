using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Collections.Internal
{
    internal class FamilySymbolComparer : IComparer<FamilySymbol>
    {
        public int Compare(FamilySymbol x, FamilySymbol y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}