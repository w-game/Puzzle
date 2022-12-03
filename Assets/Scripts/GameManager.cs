using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static User User { get; } = new();
    void Awake()
    {
        UIManager.Instance.PushMain<HomeViewData>();
        User.Init();
    }
}
