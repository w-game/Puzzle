using TMPro;
using UnityEngine;

namespace UI.View
{
    public class SplashView : ViewBase
    {
        [SerializeField] private TextMeshProUGUI version;
        private void Awake()
        {
            version.text = $"Version {Application.version}";
        }
    }
}