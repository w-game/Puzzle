using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class PlayerPrefsX
    {
        public static void SetIntArray(string key, int[] array)
        {
            string dataStr = "";
            for (int i = 0; i < array.Length; i++)
            {
                dataStr += array[i].ToString();
                if (i != array.Length - 1)
                {
                    dataStr += ",";
                }
            }

            PlayerPrefs.SetString(key, dataStr);
            SLog.D("PlayerPrefsX Demo", dataStr);
        }

        public static int[] GetIntArray(string key, int[] defaultValue)
        {
            var arrayStr = PlayerPrefs.GetString(key, string.Empty);
            if (arrayStr == string.Empty)
            {
                return defaultValue;
            }

            List<int> list = new List<int>();

            string[] dataStrs = arrayStr.Split(',');

            foreach (var str in dataStrs)
            {
                var data = int.Parse(str);
                list.Add(data);
            }
            
            return list.ToArray();
        }
    }
}