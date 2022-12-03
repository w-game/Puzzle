using UnityEngine;
using UnityEngine.UI;

public abstract class Block : MonoBehaviour
{
    protected Image _img;
    private Color _pattern;
    public Color Pattern
    {
        get => _pattern;
        protected set
        {
            _pattern = value;
            _img.color = value;
        }
    }

    public void Init()
    {
        _img = GetComponent<Image>();
        CalcPattern();
    }

    /// <summary>
    /// 计算图案
    /// </summary>
    protected abstract void CalcPattern();
}