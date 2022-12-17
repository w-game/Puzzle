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
        private LevelGoal _levelGoal;

        public void SetGoal(LevelGoal levelGoal)
        {
            GenerateItem(levelGoal);
        }

        private void GenerateItem(LevelGoal levelGoal)
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();
            
            _levelGoal = levelGoal;
            AddressableMgr.Load<GameObject>("Prefabs/UI/Element/" + levelGoal.ElementPath, p =>
            {
                foreach (var color in levelGoal.GoalCount.Keys)
                {
                    var item = Instantiate(p, content).GetComponent<LevelGoalItemElement>();
                    item.Init(color, levelGoal.GoalCount[color]);
                    _items.Add(item);
                }
            });
        }

        public void RefreshGoal()
        {
            foreach (var item in _items)
            {
                item.SetData(_levelGoal.GetRemainCount(item.Color));
            }
        }
    }
}