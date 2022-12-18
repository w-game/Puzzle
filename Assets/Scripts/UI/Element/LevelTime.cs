using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelTime : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private float _maxTime;
        private float _count;
        private Action _onTimeEnd;

        private bool _switch;
        public void SetTime(float maxTime, Action onTimeEnd)
        {
            _count = _maxTime = maxTime;
            _onTimeEnd = onTimeEnd;
            _switch = true;
        }

        private void Update()
        {
            if (_switch)
            {
                _count -= Time.deltaTime;
                if (_count <= 0)
                {
                    _switch = false;
                    _onTimeEnd?.Invoke();
                }

                slider.value = _count / _maxTime;
            }
        }

        public void Stop()
        {
            _switch = false;
        }
    }
}