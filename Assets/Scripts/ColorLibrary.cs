using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ColorLibrary
    {
        public static List<List<string>> FirstColorCoder = new()
        {
            new List<string>()
            {
                "#F7D967",
                "#3DB3D0",
                "#C24347",
                "#1E588D"
            },
            new List<string>()
            {
                "#90AFC5",
                "#336B87",
                "#2A3132",
                "#763626"
            },
            new List<string>()
            {
                "#505160",
                "#68829E",
                "#AEBD38",
                "#598234"
            },
            new List<string>()
            {
                "#003B46",
                "#07575B",
                "#66A5AD",
                "#C4DFE6"
            },
            new List<string>()
            {
                "#021C1E",
                "#004445",
                "#2C7873",
                "#6FB98F"
            },
            new List<string>()
            {
                "#98DBC6",
                "#5BC8AC",
                "#E6D72A",
                "#F18D9E"
            },
            new List<string>()
            {
                "#4CB5F5",
                "#B7B8B6",
                "#34675C",
                "#B3C100"
            },
            new List<string>()
            {
                "#F4CC70",
                "#DE7A22",
                "#20948B",
                "#6AB187"
            },
            new List<string>()
            {
                "#9A9EAB",
                "#5D535E",
                "#EC96A4",
                "#DFE166"
            },
            new List<string>()
            {
                "#EB8A44",
                "#F9DC24",
                "#4B7447",
                "#8EBA43"
            },
            new List<string>()
            {
                "#F52549",
                "#FA6775",
                "#FFD64D",
                "#9BC01C"
            },
            new List<string>()
            {
                "#2E2300",
                "#6E6702",
                "#C05805",
                "#DB9501"
            },
            new List<string>()
            {
                "#50312F",
                "#CB0000",
                "#E4EA8C",
                "#3F6C45"
            },
            new List<string>()
            {
                "#4D85BD",
                "#7CAA2D",
                "#F5E356",
                "#CB6318"
            }
        };

        public static List<string> ThemeColorCoder { get; private set; }

        public static List<string> RandomColorCoder = new()
        {
            "#5BAB3C",
            "#EBA4D2",
            "#4A42AE",
            "#B74E6A",
            "#6AAFF2",
            "#CC54D0",
            "#FD8326",
            "#F59192",
            "#F4545E",
            "#D32729",
            "#F38E59",
            "#E8A79D",
            "#AF8792",
            "#A84D41",
            "#97E3FE",
            "#F394F8",
            "#818DE0",
            "#9A53D0",
            "#2843AD"
        };
        // public static List<string> ColorCoder = new()
        // {
        //     "#F7D967",
        //     "#3DB3D0",
        //     "#C24347",
        //     "#1E588D",
        // };
        public static void InitThemeColorCoder()
        {
            ThemeColorCoder = FirstColorCoder[Random.Range(0, FirstColorCoder.Count)];
        }
    }
}