using Android;
using Common;
using UnityEngine;

namespace UI
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private GameObject mask;
        [SerializeField] private Transform content;

        private ViewStack _viewStack;
        public ViewBase SubView { get; private set; }
        
        public ViewData ViewData { get; private set; }
        public void CreateSubView(ViewData viewData, ViewStack stack, params object[] objects)
        {
            if (viewData.Mask)
            {
                mask.SetActive(true);
            }

            string path = (viewData.ViewType == ViewType.Popup ? "Prefabs/UI/Popup/" : "Prefabs/UI/View/") + viewData.ViewName;
            AddressableMgr.Load<GameObject>(path, prefab =>
            {
                var go = Instantiate(prefab, content);
                SubView = go.GetComponent<ViewBase>();
                SubView.BaseView = this;
                ViewData = viewData;

#if UNITY_IOS
                SubView.ScreenAdapt(Screen.safeArea);
#elif UNITY_ANDROID
                var safeArea = Screen.safeArea;
                SubView.ScreenAdapt(new Rect(0, AndroidNative.GetNotchHeight(), safeArea.width, safeArea.height));
#endif
                SubView.Localization();
                if (viewData.AnimaSwitch)
                {
                    SubView.DoOpenAnima();
                }
                SubView.OnCreate(objects);
                _viewStack = stack;
            });
        }

        public void CloseView()
        {
            SubView.OnClose();
            if (ViewData.AnimaSwitch)
            {
                SubView.DoCloseAnima(() =>
                {
                    _viewStack.Pop(this);
                });
            }
            else
            {
                _viewStack.Pop(this);
            }
        }
    }
}