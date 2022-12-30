namespace GameMode.EndlessGame
{
    public class PowerReward : Reward
    {
        public override string Tip => GameManager.Language.PowerRewardTip;

        public override void Execute()
        {
            GameManager.User.IncreasePower(1);
        }
    }
}