using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        UIManager.Instance.PushMain<HomeViewData>();
    }
}
