using Common;
using GameSystem;

public class GameManager : MonoSingleton<GameManager>
{
    public static User User { get; } = new();

    public ChallengeSystem ChallengeSystem { get; } = new();
    void Awake()
    {
        UIManager.Instance.PushMain<HomeViewData>();
        User.Init();
        AdManager.Instance.Init();
    }
}
