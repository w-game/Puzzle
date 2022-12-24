using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        private Image _img;
    
        private Image _specialIcon;
    
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
    
        public virtual bool CanMove => true;
        public virtual bool CanRemove => true;
    
        public void Init(Color color)
        {
            _img = transform.Find("Icon").GetComponent<Image>();
            _specialIcon = transform.Find("SpecialIcon").GetComponent<Image>();
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
        
        protected void SetSpecialIcon(string path)
        {
            AddressableMgr.Load<Sprite>(path, sprite =>
            {
                _specialIcon.sprite = sprite;
                _specialIcon.gameObject.SetActive(true);
            });
        }
    
        public virtual void OnPlaced() { }
        public virtual void OnRemove() { }
    }
}