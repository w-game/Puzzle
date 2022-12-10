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
    [SerializeField] private Button fpsBtn;
    [SerializeField] private Button completeBtn;
    public override void OnCreate(params object[] objects)
    {
        bgm.onValueChanged.AddListener(OnBgmValueChanged);
        soundEffect.onValueChanged.AddListener(OnSoundEffectValueChanged);
        fpsBtn.onClick.AddListener(OnFpsBtnClick);
        completeBtn.onClick.AddListener(CloseView);
        
        Init();
    }

    private void Init()
    {
        fpsSlider.value = Application.targetFrameRate == 60 ? 1 : 0;
        bgm.value = SoundManager.Instance.BgmVolume;
        soundEffect.value = SoundManager.Instance.SoundEffectVolume;
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
}