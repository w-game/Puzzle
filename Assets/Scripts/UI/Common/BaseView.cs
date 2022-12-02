using System;
using Common;
using UnityEngine;

namespace UI
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private GameObject mask;
        [SerializeField] private Transform content;

        private ViewStack _viewStack;
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
                var subView = go.GetComponent<ViewBase>();
                subView.BaseView = this;

                subView.OnCreate(objects);
                _viewStack = stack;
            });
        }

        internal void CloseView()
        {
            _viewStack.Pop(this);
        }
    }
}