using System;
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

        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI nextBlockTipUpper;

        public override void Localization()
        {
            nextBlockTipUpper.text = GameManager.Language.NextNewBlockTipUpper;
        }

        protected override void Refresh()
        {
            base.Refresh();
            nextBlockScore.text = String.Format(GameManager.Language.NextNewBlockTipBottom, puzzleGame.NextBlockScore - puzzleGame.Score);
        }
    }
}