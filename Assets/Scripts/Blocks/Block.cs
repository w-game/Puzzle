using Common;
using UnityEngine;
using UnityEngine.UI;

public abstract class Block : MonoBehaviour
{
    private Image _img;

    protected Transform SpecialFrame;
    private Image _specialIcon;

    public int Pattern { get; protected set; } = -1;

    public void Init()
    {
        _img = transform.Find("Icon").GetComponent<Image>();
        SpecialFrame = transform.Find("SpecialFrame");
        _specialIcon = transform.Find("SpecialIcon").GetComponent<Image>();
        CalcPattern();
    }

    protected void SetPattern(string path)
    {
        AddressableMgr.Load<Sprite>(path, sprite =>
        {
            _img.sprite = sprite;
        });
    }
    
    protected void SetSpecialIcon(string path)
    {
        AddressableMgr.Load<Sprite>(path, sprite =>
        {
            _specialIcon.sprite = sprite;
            _specialIcon.gameObject.SetActive(true);
        });
    }

    /// <summary>
    /// 计算图案
    /// </summary>
    protected abstract void CalcPattern();

    public virtual void OnPlaced() { }
    public virtual void OnRemove() { }
}