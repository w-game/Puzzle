using Ad;
using Common;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PopSettingData : ViewData
{
    public override string ViewName => "PopSetting";
    public override ViewType ViewType => ViewType.Popup;
    public override bool Mask => true;
    public override bool AnimaSwitch => true;
}

public class PopSetting : PopupBase
{
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider soundEffect;
    [SerializeField] private Slider fpsSlider;
    [SerializeField] private Slider adSlider;
    [SerializeField] private Button fpsBtn;
    [SerializeField] private Button adBtn;
    [SerializeField] private Button completeBtn;
    public override void OnCreate(params object[] objects)
    {
        bgm.onValueChanged.AddListener(OnBgmValueChanged);
        soundEffect.onValueChanged.AddListener(OnSoundEffectValueChanged);
        fpsBtn.onClick.AddListener(OnFpsBtnClick);
        completeBtn.onClick.AddListener(CloseView);
        adBtn.onClick.AddListener(OnAdBtnClicked);
        
        Init();
    }

    private void Init()
    {
        fpsSlider.value = Application.targetFrameRate == 60 ? 1 : 0;
        bgm.value = SoundManager.Instance.BgmVolume;
        soundEffect.value = SoundManager.Instance.SoundEffectVolume;
        adSlider.value = AdManager.Instance.NativeAdSwitch ? 1 : 0;
    }

    private void OnBgmValueChanged(float value)
    {
        SoundManager.Instance.ChangeBgmVolume(value);
    }
    
    private void OnSoundEffectValueChanged(float value)
    {
        SoundManager.Instance.ChangeSoundEffectVolume(value);
    }

    private void OnFpsBtnClick()
    {
        fpsSlider.value = 1 - fpsSlider.value < 0.1f ? 0 : 1;
        Application.targetFrameRate = fpsSlider.value > 0.9f ? 60 : 30;
    }

    private void OnAdBtnClicked()
    {
        if (adSlider.value > 0.5f)
        {
            UIManager.Instance.ShowCheckBox("关闭展示弹窗广告将减少开发者收入，是否确认关闭？", SetNativeSwitch);
        }
        else
        {
            SetNativeSwitch();
        }
    }

    private void SetNativeSwitch()
    {
        adSlider.value = adSlider.value > 0.5f ? 0 : 1;
        AdManager.Instance.NativeAdSwitch = adSlider.value > 0.5f;
    }
}