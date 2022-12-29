using TMPro;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI version;
    private void Awake()
    {
        version.text = $"Version {Application.version}";
    }
}