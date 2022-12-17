using Revit.Elements;
using DB = Autodesk.Revit.DB;

namespace Parameters
{
    public class ParameterData
    {
        public Element? Element { get; set; }
        public DB.Parameter? Parameter { get; set; }
        public string? ParameterName { get; set; }
        public DB.ParameterType? ParameterType { get; set; }
        public double Double { get; set; }
        public DB.ElementId? ElementId { get; set; }
        public int Integer { get; set; }
        public string? String { get; set; }
    }
}
