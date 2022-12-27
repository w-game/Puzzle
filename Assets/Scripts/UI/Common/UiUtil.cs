using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public static class UiUtil
    {
        public static void SetImage(this Image image, string path)
        {
            AddressableMgr.Load<Sprite>(path, sprite =>
            {
                if (image)
                {
                    image.sprite = sprite;
                }
            });
        }
    }
}