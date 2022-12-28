using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class SButton : Button
    {
        private TextMeshProUGUI _btnTxt;
        protected override void Awake()
        {
            base.Awake();
            _btnTxt = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetButtonText(string text)
        {
            _btnTxt.text = text;
        }
    }
}