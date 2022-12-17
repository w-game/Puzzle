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
        private ViewBase _subView;
        
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
                _subView = go.GetComponent<ViewBase>();
                _subView.BaseView = this;
                ViewData = viewData;

#if UNITY_IOS
                _subView.ScreenAdapt(Screen.safeArea);
#elif UNITY_ANDROID
                var safeArea = Screen.safeArea;
                _subView.ScreenAdapt(new Rect(0, AndroidNative.GetNotchHeight(), safeArea.width, safeArea.height));
#endif
                if (viewData.AnimaSwitch)
                {
                    _subView.DoOpenAnima();
                }
                _subView.OnCreate(objects);
                _viewStack = stack;
            });
        }

        public void CloseView()
        {
            if (ViewData.AnimaSwitch)
            {
                _subView.DoCloseAnima(() =>
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