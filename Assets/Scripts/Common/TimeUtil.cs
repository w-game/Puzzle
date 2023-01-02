using System;
using DG.Tweening;
using UnityEngine.Events;

namespace Common
{
    public class TimeUtil
    {
        public static int Timestamp => (int)((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);

        public static void Timer(float time, UnityAction callback)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(time);
            sequence.AppendCallback(() => callback?.Invoke());
        }
    }
}