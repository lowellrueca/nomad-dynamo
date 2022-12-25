using System.Collections.Generic;
using Revit.Elements;

namespace NomadCore.Repository
{
    public interface IElementRepository<T>
    {
        IEnumerable<T> GetElementsByCategory(Category category);
    }
}
