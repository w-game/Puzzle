using System.Collections.Generic;
using TapTap.TapDB;

namespace Common
{
    public class SEvent
    {
        public static void TrackEvent(string eventName, string properties)
        {
            SLog.D("Remote Event", $"上报事件 [{eventName}]\n{properties}");
            TapDB.TrackEvent(eventName, properties);
        }

        public static void TrackEvent(string eventName, Dictionary<string, object> properties)
        {
            var pps = "{\n";
            foreach (var propertyName in properties.Keys)
            {
                pps += $"\t{propertyName}: {properties[propertyName]}\n";
            }
            pps += "}";
            
            SLog.D("Remote Event", $"上报事件 [{eventName}]\n{pps}");
            TapDB.TrackEvent(eventName, properties);
        }
    }
}