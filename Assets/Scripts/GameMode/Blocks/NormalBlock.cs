using UnityEngine;

namespace Blocks
{
    public class NormalBlock : Block
    {
        protected override void SetPattern(Color color)
        {
            Pattern = color;
        }

        public override void ShowSpecialIcon(Sprite sprite, bool changeColor)
        {
            SpecialIcon.sprite = sprite;
            SpecialIcon.gameObject.SetActive(true);
            
            if (changeColor)
            {
                var color = SpecialIcon.color;
                if (color.r + color.g + color.b < 1.5f)
                {
                    var c = Pattern * 1.5f;
                    c.a = 1;
                    SpecialIcon.color = c;
                }
                else
                {
                    var c = Pattern * 1.5f;
                    c.a = 1;
                    SpecialIcon.color = c;
                }
            }
        }

        public override void HideSpecialIcon()
        {
            SpecialIcon.gameObject.SetActive(false);
            SpecialIcon.color = Color.white;
        }
    }
}