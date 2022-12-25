using System.Collections.Generic;
using GameMode.LevelGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelGoalItemElement : MonoBehaviour
    {
        [SerializeField] private List<Image> icons;
        [SerializeField] private TextMeshProUGUI countTxt;

        private LevelGoal _levelGoal;
        public void Init(LevelGoal levelGoal)
        {
            _levelGoal = levelGoal;
            foreach (var icon in icons)
            {
                icon.color = _levelGoal.Pattern;
                if (!string.IsNullOrEmpty(_levelGoal.SpritePath))
                {
                    icon.SetImage(_levelGoal.SpritePath);
                }
            }
            
            RefreshData();
        }
        
        public void RefreshData()
        {
            countTxt.text = $"x{_levelGoal.RemainCount}";
        }
    }
}