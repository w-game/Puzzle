using System.Collections.Generic;
using TapTap.TapDB;

namespace Common
{
    public class SEvent
    {
        public static void TrackEvent(string eventName, Dictionary<string, object> properties = null)
        {
            if (properties != null)
            {
                var pps = "{\n";
                foreach (var propertyName in properties.Keys)
                {
                    pps += $"\t{propertyName}: {properties[propertyName]}\n";
                }
                pps += "}";
                SLog.D("Remote Event", $"上报事件 [{eventName}]\n{pps}");
            }
            else
            {
                properties = new Dictionary<string, object>();
                SLog.D("Remote Event", $"上报事件 [{eventName}] properties: null");
            }
            
            TapDB.TrackEvent(eventName, properties);
        }
    }
}