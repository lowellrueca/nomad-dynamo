using System.Collections.Generic;
using Revit.Elements;

namespace Collections.Internal
{
    internal class FamilyTypeComparer : IComparer<FamilyType>
    {
        public int Compare(FamilyType x, FamilyType y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}