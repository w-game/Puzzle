using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    private Image img;
    private Color _pattern;
    public Color Pattern
    {
        get => _pattern;
        set
        {
            _pattern = value;
            if (img == null) img = GetComponent<Image>();
            img.color = value;
        }
    }
}