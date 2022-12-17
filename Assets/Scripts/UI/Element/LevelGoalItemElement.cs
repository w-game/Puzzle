using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelGoalItemElement : MonoBehaviour
    {
        [SerializeField] private List<Image> icons;
        [SerializeField] private TextMeshProUGUI countTxt;

        public Color Color { get; private set; }
        public void Init(Color color, int count)
        {
            Color = color;
            foreach (var icon in icons)
            {
                icon.color = color;
            }
            SetData(count);
        }
        
        public void SetData(int count)
        {
            countTxt.text = $"x{count}";
        }
    }
}