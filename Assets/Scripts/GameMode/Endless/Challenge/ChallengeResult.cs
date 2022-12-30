namespace GameMode.EndlessGame
{
    public abstract class ChallengeResult
    {
        public abstract string Tip { get; }
        public abstract void Execute();
    }
}