using System.Collections.Generic;
using Common;
using GameMode.LevelGame;
using UnityEngine;

namespace UI
{
    public class LevelGoalElement : MonoBehaviour
    {
        [SerializeField] private Transform content;

        private List<LevelGoalItemElement> _items = new();
        private List<LevelGoal> _levelGoals;

        public void SetGoal(List<LevelGoal> levelGoals)
        {
            GenerateItem(levelGoals);
        }

        private void GenerateItem(List<LevelGoal> levelGoals)
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();
            
            _levelGoals = levelGoals;
            foreach (var levelGoal in _levelGoals)
            {
                AddressableMgr.Load<GameObject>("Prefabs/UI/Element/" + levelGoal.ElementPath, p =>
                {
                    var item = Instantiate(p, content).GetComponent<LevelGoalItemElement>();
                    item.Init(levelGoal);
                    _items.Add(item);
                });
            }
        }

        public void RefreshGoal()
        {
            foreach (var item in _items)
            {
                item.RefreshData();
            }
        }
    }
}