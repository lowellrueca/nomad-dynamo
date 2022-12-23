using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Collections.Internal
{
    internal class FamilyComparer : IComparer<Family>
    {
        public int Compare(Family x, Family y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}