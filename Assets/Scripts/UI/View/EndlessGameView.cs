using System;
using System.Collections.Generic;
using DG.Tweening;
using GameMode.EndlessGame;
using GameMode.LevelGame;
using TMPro;
using UnityEngine;

namespace UI.View
{
    public class EndlessGameViewData : ViewData
    {
        public override string ViewName => "UnlimitedGameView";

        public override ViewType ViewType => ViewType.View;

        public override bool Mask => true;
    }
    
    public class EndlessGameView : GameView
    {
        public enum EndlessEventKeys
        {
            SetChallenge,
            EndChallenge,
            ShowChallengeResult
        }
        [SerializeField] private TextMeshProUGUI nextBlockScore;
        [SerializeField] private Transform roundCountTrans;
        [SerializeField] private TextMeshProUGUI roundCountTitle;
        [SerializeField] private CanvasGroup challengeEffect;
        [SerializeField] private ChallengeResultElement challengeResult;

        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI nextBlockTipUpper;

        public override void OnCreate(params object[] objects)
        {
            base.OnCreate(objects);
            AddEvent<List<LevelGoal>>(EndlessEventKeys.SetChallenge, SetChallenge);
            AddEvent(EndlessEventKeys.EndChallenge, EndChallenge);
            AddEvent<ChallengeResult>(EndlessEventKeys.ShowChallengeResult, ShowChallengeResult);
        }

        public override void Localization()
        {
            roundCountTitle.text = GameManager.Language.RoundCountLabel;
            nextBlockTipUpper.text = GameManager.Language.NextNewBlockTipUpper;
        }

        private void SetChallenge(List<LevelGoal> goals)
        {
            UIManager.Instance.SetTopMask(true);
            goalElement.SetGoal(goals);
            goalElement.gameObject.SetActive(true);
            var goalTrans = goalElement.transform;
            goalTrans.position = transform.position;
            goalTrans.localScale = Vector3.zero;
            var sequence = DOTween.Sequence();
            sequence.Append(goalTrans.DOScale(1, 0.4f));
            sequence.AppendInterval(1f);
            sequence.Append(challengeEffect.DOFade(0, 0.4f));
            sequence.Append(goalElement.transform.DOLocalMove(Vector3.zero, 0.5f));
            sequence.AppendCallback(() =>
            {
                roundCountTrans.gameObject.SetActive(true);
            });
            sequence.Append(roundCountTrans.DOLocalMove(Vector3.down * 128f, 0.4f));
            sequence.AppendCallback(() =>
            {
                UIManager.Instance.SetTopMask(false);
            });
        }

        private void EndChallenge()
        {
            challengeEffect.alpha = 1;
            goalElement.gameObject.SetActive(false);
            roundCountTrans.gameObject.SetActive(false);
            roundCountTrans.localPosition = Vector3.zero;
        }

        private void ShowChallengeResult(ChallengeResult result)
        {
            challengeResult.SetData(result);
        }

        protected override void Refresh()
        {
            base.Refresh();
            nextBlockScore.text = String.Format(GameManager.Language.NextNewBlockTipBottom, puzzleGame.NextBlockScore - puzzleGame.Score);
        }
    }
}