using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class MainViewData : ViewData
    {
        public override string ViewName => "MainView";
        public override ViewType ViewType => ViewType.View;
        public override bool Mask => false;
    }
    public class MainView : ViewBase
    {
        public override void OnCreate(params object[] objects)
        {

        }
    }
}