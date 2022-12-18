using Data.Internal;

namespace Data
{
    public static class Timeline
    {
        public static string DateTime { 
            get
            {
                return DataManager.GetCurrentDateTime();
            } 
        }    
    }
}
