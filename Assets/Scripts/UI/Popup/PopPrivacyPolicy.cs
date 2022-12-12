using System;
using Common;
using UI;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PopPrivacyPolicyData : ViewData
{
    public override string ViewName => "PopPrivacyPolicy";
    public override ViewType ViewType => ViewType.Popup;
    public override bool Mask => true;
    public override bool AnimaSwitch => true;
}

public class PopPrivacyPolicy : PopupBase
{
    [SerializeField] private Button btn;

    private static readonly string[] Permissions = {
        "android.permission.READ_PHONE_STATE",
        "android.permission.WRITE_EXTERNAL_STORAGE",
        "android.permission.READ_EXTERNAL_STORAGE"
        // "android.permission.INTERNET",
        // "android.permission.ACCESS_NETWORK_STATE",
        // "android.permission.REQUEST_INSTALL_PACKAGES"
    };

    private static readonly string[] RequirePermission =
    {
        // "android.permission.READ_PHONE_STATE",
        // "android.permission.WRITE_EXTERNAL_STORAGE",
        // "android.permission.READ_EXTERNAL_STORAGE"
    };

    private bool _requestPermission;

    public override void OnCreate(params object[] objects)
    {
        btn.onClick.AddListener(RequestPermission);
    }

    private void RequestPermission()
    {
        _requestPermission = true;
        Permission.RequestUserPermissions(Permissions);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SLog.D("Privacy Policy", hasFocus);
        if (hasFocus)
        {
            if (_requestPermission)
            {
                foreach (var permission in RequirePermission)
                {
                    var result = Permission.HasUserAuthorizedPermission(permission);
                    SLog.D("Privacy Policy", $"{permission} ({result})");
                    if (!result)
                    {
                        _requestPermission = false;
                        UIManager.Instance.ShowToast("权限获取失败，无法进入游戏");
                        return;
                    }
                }
                
                CloseView();
                UIManager.Instance.CloseSplash();
                GameManager.User.PrivacyPolicy = false;
            }
        }
    }
}