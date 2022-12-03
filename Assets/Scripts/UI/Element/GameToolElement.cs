using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameToolElement : Element
    {
        [SerializeField] private GameToolName tool;
        private Button _btn;
        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(CheckUseTool);
        }

        private void CheckUseTool()
        {
            var gameTool = GameManager.User.Tools[tool];
            gameTool.CheckUse();
        }
    }
}