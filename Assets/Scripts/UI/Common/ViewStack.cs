using System.Collections.Generic;
using Common;
using UnityEngine;

namespace UI
{
    public class ViewStack : MonoBehaviour
    {
        private readonly List<BaseView> _views = new();

        public void Push<T>(params object[] objects) where T : ViewData, new()
        {
            var viewData = new T();

            AddressableMgr.Load<GameObject>("Prefabs/UI/BaseView", prefab =>
            {
                var go = Instantiate(prefab, transform);
                var view = go.GetComponent<BaseView>();
                view.CreateSubView(viewData, this, objects);
                _views.Add(view);
            });
        }

        public void Pop(BaseView baseView)
        {
            _views.Remove(baseView);
            Destroy(baseView.gameObject);
        }

        public void Pop<T>() where T : ViewData
        {
            var baseView = _views.Find(_ => _.ViewData is T);
            if (baseView)
            {
                baseView.CloseView();
            }
        }
    }
}