using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        private Image _img;
    
        private Color _pattern;
    
        public Color Pattern
        {
            get => _pattern;
            protected set
            {
                _pattern = value;
                _img.color = value;
            }
        }
        
        public Image SpecialIcon { get; private set; }
    
        public virtual bool CanMove => true;
        public virtual bool CanRemove => true;
        public virtual bool MainBlock => true;
        
        public BlockSlot Slot { get; set; }
    
        public void Init(Color color, BlockSlot slot)
        {
            _img = transform.Find("Icon").GetComponent<Image>();
            SpecialIcon = _img.transform.Find("SpecialIcon").GetComponent<Image>();
            Slot = slot;
            SetPattern(color);
        }
    
        protected abstract void SetPattern(Color color);
    
        protected void SetIcon(string path)
        {
            AddressableMgr.Load<Sprite>(path, sprite =>
            {
                _img.sprite = sprite;
            });
        }
        
        protected void SetSpecialIcon(string path, Color color)
        {
            AddressableMgr.Load<Sprite>(path, sprite =>
            {
                SpecialIcon.sprite = sprite;
                SpecialIcon.color = color;
                SpecialIcon.gameObject.SetActive(true);
            });
        }

        public virtual void OnRoundEnd() { }
        public virtual void OnPlaced() { }
        public virtual void OnRemove() { }

        public void DoRemoveAnima(TweenCallback callback)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(0, PuzzleGame.AnimaTime).SetEase(Ease.InQuad));
            sequence.AppendCallback(callback);
        }
        
        public virtual void ShowSpecialIcon(Sprite sprite, bool changeColor) { }

        public virtual void HideSpecialIcon() { }
    }
}