using Common;
using GameSystem;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static User User { get; } = new();
    
    public PuzzleGame GameMode { get; set; }

    public ChallengeSystem ChallengeSystem { get; } = new();
    void Awake()
    {
        UIManager.Instance.PushMain<HomeViewData>();
        User.Init();
        SoundManager.Instance.Init();
        Application.targetFrameRate = 60;
    }
}
