using TMPro;
using UnityEngine;

namespace UI.View
{
    public class UnlimitedGameViewData : ViewData
    {
        public override string ViewName => "UnlimitedGameView";

        public override ViewType ViewType => ViewType.View;

        public override bool Mask => true;
    }
    
    public class UnlimitedGameView : GameView
    {
        [SerializeField] private TextMeshProUGUI nextBlockScore;

        protected override void Refresh()
        {
            base.Refresh();
            nextBlockScore.text = $"还差<color=#C24347>{puzzleGame.NextBlockScore - puzzleGame.Score}</color>分";
        }
    }
}