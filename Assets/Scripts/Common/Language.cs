using System.Collections.Generic;

namespace Common
{
    public enum ELanguage
    {
        SimplifiedChinese,
        TraditionalChinese,
        English
    }
    public abstract class LanguageBase
    {
        public static Dictionary<ELanguage, LanguageBase> Languages = new()
        {
            { ELanguage.SimplifiedChinese, new SimplifiedChinese() },
            { ELanguage.TraditionalChinese, new TraditionalChinese() },
            { ELanguage.English, new English() },
        };

        public abstract ELanguage L { get; }
        public abstract string SimplifiedChineseText { get; }
        public abstract string TraditionalChineseText { get; }
        public abstract string EnglishText { get; }
        public abstract string LanguageTitle { get; }
        public abstract string LevelModeStartBtn { get; }
        public abstract string UnlimitedModeStartBtn { get; }
        public abstract string AllRemoveTxt { get; }
        public abstract string Level { get; }
        public abstract string NextNewBlockTipUpper { get; }
        public abstract string NextNewBlockTipBottom { get; }
        public abstract string GameOverText { get; }
        public abstract string Restart { get; }
        public abstract string Revive { get; }
        public abstract string ToolNotEnoughTitle { get; }
        public abstract string ToolNotEnoughDes { get; }
        public abstract string ToolClearSlotsName { get; }
        public abstract string ToolClearControlName { get; }
        public abstract string GetToolBtnText { get; }
        public abstract string CancelText { get; }
        public abstract string LevelFailTitle { get; }
        public abstract string LevelFailDes { get; }
        public abstract string BackToHomeText { get; }
        public abstract string NewSpecialBlockTitle { get; }
        public abstract string SureText { get; }
        public abstract string GameIntroductionTitle { get; }
        public abstract string GameIntroductionDes { get; }
        public abstract string GotItText { get; }
        public abstract string LevelSuccessTitle { get; }
        public abstract string NextLevelText { get; }
        public abstract string PowerNotEnoughTitle { get; }
        public abstract string PowerNotEnoughDes { get; }
        public abstract string GetPowerBtnText { get; }
        public abstract string GetPowerTip { get; }
        public abstract string GameSettingTitle { get; }
        public abstract string SoundText { get; }
        public abstract string BGMText { get; }
        public abstract string SoundEffectText { get; }
        public abstract string GameText { get; }
        public abstract string FPSText { get; }
        public abstract string AdText { get; }
        public abstract string PopupAdText { get; }
        public abstract string Done { get; }
        public abstract string TipTitle { get; }
        public abstract string CheckBoxTitle { get; }
        public abstract string Confirm { get; }
        public abstract string BackToHomeCheckDes { get; }
        public abstract string ClosePopupAdCheckDes { get; }
        public abstract string LevelEndTipDes { get; }
        public abstract string GetPermissionFail { get; }
        public abstract string RewardLabel { get; }
        public abstract string PunishmentLabel { get; }
        public abstract string PowerRewardTip { get; }
        public abstract string ClearSingleColorAllBlocksRewardTip { get; }
        public abstract string ClearBottomRowAllBlocksRewardTip { get; }
        public abstract string GenerateNewRowAtBottomPunishmentTip { get; }
        public abstract string GenerateRandomNormalBlockAtTopPunishmentTip { get; }
        public abstract string ToastInfoType { get; }
        public abstract string ToastWarningType { get; }
        public abstract string ToastErrorType { get; }
        public abstract string RoundCountLabel { get; }
        public abstract string GetRewardTip { get; }
        public abstract string AdLoadFail { get; }
    }

    public class SimplifiedChinese : LanguageBase
    {
        public override ELanguage L => ELanguage.SimplifiedChinese;
        public override string SimplifiedChineseText => "中文（简体）";
        public override string TraditionalChineseText => "中文（繁体）";
        public override string EnglishText => "英文";
        public override string LanguageTitle => "语言";
        public override string LevelModeStartBtn => "关卡模式";
        public override string UnlimitedModeStartBtn => "无尽模式";
        public override string AllRemoveTxt => "共完成过{0}次全部消除";
        public override string Level => "第{0}关";
        public override string NextNewBlockTipUpper => "距离下一个新方块";
        public override string NextNewBlockTipBottom => "还差<color=#C24347>{0}</color>分";
        public override string GameOverText => "游戏结束";
        public override string Restart => "再次挑战";
        public override string Revive => "复活继续";
        public override string ToolNotEnoughTitle => "道具不足";
        public override string ToolNotEnoughDes => "道具「{0}」数量不足，无法使用！";
        public override string ToolClearSlotsName => "清空棋盘";
        public override string ToolClearControlName => "重置控制栏";
        public override string GetToolBtnText => "获得一个道具";
        public override string CancelText => "取消";
        public override string LevelFailTitle => "过关失败";
        public override string LevelFailDes => "还差一点就过关了！！！";
        public override string BackToHomeText => "返回大厅";
        public override string NewSpecialBlockTitle => "新增特殊方块";
        public override string SureText => "确定";
        public override string GameIntroductionTitle => "玩法说明";
        public override string GameIntroductionDes => "1. 拖动下方控制栏中的小方块可以控制下次方块的生成位置。调整完成后，点击下方小人后，会在游戏棋盘中生成方块落下。\n" +
                                                      "2. 当新生成的方块落下后，游戏棋盘中如果存在三个及以上相邻的方块，则消除这些色块，从而获得分数。";
        public override string GotItText => "我会了";
        public override string LevelSuccessTitle => "恭喜过关";
        public override string NextLevelText => "下一关";
        public override string PowerNotEnoughTitle => "体力不足";
        public override string PowerNotEnoughDes => "每10分钟会恢复1点体力";
        public override string GetPowerBtnText => "获得5点体力";
        public override string GetPowerTip => "恭喜获得5点体力！";
        public override string GameSettingTitle => "游戏设置";
        public override string SoundText => "声音";
        public override string BGMText => "背景音乐";
        public override string SoundEffectText => "音效";
        public override string GameText => "游戏";
        public override string FPSText => "30/60帧";
        public override string AdText => "广告";
        public override string PopupAdText => "弹窗广告";
        public override string Done => "完成";
        public override string TipTitle => "提示";
        public override string CheckBoxTitle => "需要确认";
        public override string Confirm => "确认";
        public override string BackToHomeCheckDes => "退出后进入关卡后的所有操作都不会保存，是否确认退出？";
        public override string ClosePopupAdCheckDes => "关闭展示弹窗广告将减少开发者收入，是否确认关闭？";
        public override string LevelEndTipDes => "您已通过现有所有的关卡！敬请期待之后的关卡更新。";
        public override string GetPermissionFail => "权限获取失败，无法进入游戏";
        public override string RewardLabel => "奖励";
        public override string PunishmentLabel => "惩罚";
        public override string PowerRewardTip => "恭喜获得一点体力！";
        public override string ClearSingleColorAllBlocksRewardTip => "消除棋盘中任意一种颜色的所有方块！";
        public override string ClearBottomRowAllBlocksRewardTip => "消除最下面一行所有的方块！";
        public override string GenerateNewRowAtBottomPunishmentTip => "最下面生成一行方块！";
        public override string GenerateRandomNormalBlockAtTopPunishmentTip => "生成N个随机颜色的方块落下！";
        public override string ToastInfoType => "信息";
        public override string ToastWarningType => "警告";
        public override string ToastErrorType => "错误";
        public override string RoundCountLabel => "剩余步数";
        public override string GetRewardTip => "恭喜获得奖励！";
        public override string AdLoadFail => "广告读取失败，请重试！";
    }
    
    public class TraditionalChinese : LanguageBase
    {
        public override ELanguage L => ELanguage.TraditionalChinese;
        public override string SimplifiedChineseText => "中文（簡體）";
        public override string TraditionalChineseText => "中文（繁體）";
        public override string EnglishText => "英文";
        public override string LanguageTitle => "語言";
        public override string LevelModeStartBtn => "关卡模式";
        public override string UnlimitedModeStartBtn => "无尽模式";
        public override string AllRemoveTxt => "共完成過{0}次全部消除";
        public override string Level => "第{0}關";
        public override string NextNewBlockTipUpper => "距離下一個新方塊";
        public override string NextNewBlockTipBottom => "還差<color=#C24347>{0}</color>分";
        public override string GameOverText => "遊戲結束";
        public override string Restart => "再次挑戰";
        public override string Revive => "復活繼續";
        public override string ToolNotEnoughTitle => "道具不足";
        public override string ToolNotEnoughDes => "道具「{0}」數量不足，無法使用！";
        public override string ToolClearSlotsName => "清空棋盤";
        public override string ToolClearControlName => "重置控制欄";
        public override string GetToolBtnText => "獲得一個道具";
        public override string CancelText => "取消";
        public override string LevelFailTitle => "過關失敗";
        public override string LevelFailDes => "還差一點就過關了！ ！ ！";
        public override string BackToHomeText => "返回大廳";
        public override string NewSpecialBlockTitle => "新增特殊方塊";
        public override string SureText => "確定";
        public override string GameIntroductionTitle => "玩法說明";

        public override string GameIntroductionDes => "1. 拖動下方控制欄中的小方塊可以控制下次方塊的生成位置。調整完成後，點擊下方小人後，會在遊戲棋盤中生成方塊落下。\n" + 
                                                      "2. 當新生成的方塊落下後，遊戲棋盤中如果存在三個及以上相鄰的方塊，則消除這些方塊，從而獲得分數。";

        public override string GotItText => "我會了";
        public override string LevelSuccessTitle => "恭喜過關";
        public override string NextLevelText => "下一關";
        public override string PowerNotEnoughTitle => "體力不足";
        public override string PowerNotEnoughDes => "每10分鐘會恢復1點體力";
        public override string GetPowerBtnText => "獲得5點體力";
        public override string GetPowerTip => "恭喜獲得5點體力!";
        public override string GameSettingTitle => "遊戲設置";
        public override string SoundText => "聲音";
        public override string BGMText => "背景音樂";
        public override string SoundEffectText => "音效";
        public override string GameText => "遊戲";
        public override string FPSText => "30/60幀";
        public override string AdText => "廣告";
        public override string PopupAdText => "彈窗廣告";
        public override string Done => "完成";
        public override string TipTitle => "提示";
        public override string CheckBoxTitle => "需要確認";
        public override string Confirm => "確認";
        public override string BackToHomeCheckDes => "退出後進入關卡後的所有操作都不會保存，是否確認退出？";
        public override string ClosePopupAdCheckDes => "關閉展示彈窗廣告將減少開發者收入，是否確認關閉？";
        public override string LevelEndTipDes => "您已通過現有所有的關卡！敬請期待之後的關卡更新。";
        public override string GetPermissionFail => "權限獲取失敗，無法進入遊戲";
        public override string RewardLabel => "獎勵";
        public override string PunishmentLabel => "懲罰";
        public override string PowerRewardTip => "恭喜獲得一點體力！";
        public override string ClearSingleColorAllBlocksRewardTip => "消除棋盤中任意一種顏色的所有方塊！";
        public override string ClearBottomRowAllBlocksRewardTip => "消除最下面一行所有的方塊！";
        public override string GenerateNewRowAtBottomPunishmentTip => "最下面生成一行方塊！";
        public override string GenerateRandomNormalBlockAtTopPunishmentTip => "生成N個隨機顏色的方塊落下！";
        public override string ToastInfoType => "信息";
        public override string ToastWarningType => "警告";
        public override string ToastErrorType => "錯誤";
        public override string RoundCountLabel => "剩餘步數";
        public override string GetRewardTip => "恭喜獲得獎勵！";
        public override string AdLoadFail => "廣告讀取失敗！";
    }
    
    public class English : LanguageBase
    {
        public override ELanguage L => ELanguage.English;
        public override string SimplifiedChineseText => "Simplified Chinese";
        public override string TraditionalChineseText => "Traditional Chinese";
        public override string EnglishText => "English";
        public override string LanguageTitle => "Language";
        public override string LevelModeStartBtn => "Level Mode";
        public override string UnlimitedModeStartBtn => "Endless Mode";
        public override string AllRemoveTxt => "Completed a total of {0} times to eliminate all";
        public override string Level => "Level {0}";
        public override string NextNewBlockTipUpper => "The next new block";
        public override string NextNewBlockTipBottom => "need <color=#C24347>{0}</color> point(s)";
        public override string GameOverText => "Game Over";
        public override string Restart => "Restart";
        public override string Revive => "Continue";
        public override string ToolNotEnoughTitle => "Lack of Game Props";
        public override string ToolNotEnoughDes => "The item 「{0}」 is insufficient and cannot be used!";
        public override string ToolClearSlotsName => "Clear The Board";
        public override string ToolClearControlName => "Refresh Control Bar";
        public override string GetToolBtnText => "Get";
        public override string CancelText => "Cancel";
        public override string LevelFailTitle => "Fail";
        public override string LevelFailDes => "Only a little to pass the level!!!";
        public override string BackToHomeText => "Home";
        public override string NewSpecialBlockTitle => "New Special Block";
        public override string SureText => "OK";
        public override string GameIntroductionTitle => "Game Instruction";
        public override string GameIntroductionDes => "1. Drag the small block in the lower control bar to control the next generation position of the block(s). After the adjustment is completed, click on the human below, and block(s) will be generated on the game board to fall.\n" +
                                                      "2. When the newly generated blocks fall, if there are three or more adjacent blocks on the game board, eliminate these color blocks to get points.";
        public override string GotItText => "Got it";
        public override string LevelSuccessTitle => "Success";
        public override string NextLevelText => "Next Level";
        public override string PowerNotEnoughTitle => "Lack of Energy";
        public override string PowerNotEnoughDes => "Every 10 minutes will restore 1 point of energy.";
        public override string GetPowerBtnText => "Get (5)";
        public override string GetPowerTip => "Get 5 points of energy.";
        public override string GameSettingTitle => "Setting";
        public override string SoundText => "Sound";
        public override string BGMText => "BGM";
        public override string SoundEffectText => "Sound Effect";
        public override string GameText => "Game";
        public override string FPSText => "FPS";
        public override string AdText => "AD";
        public override string PopupAdText => "Native Ad";
        public override string Done => "Done";
        public override string TipTitle => "Tip";
        public override string CheckBoxTitle => "Check";
        public override string Confirm => "Confirm";
        public override string BackToHomeCheckDes => "If you confirm, All operations after entering the game will not be saved. Are you sure to exit?";
        public override string ClosePopupAdCheckDes => "If you confirm, developer income will reduce , are you sure to close it?";
        public override string LevelEndTipDes => "You have passed all available levels! Stay tuned for future level updates.";
        public override string GetPermissionFail => "Failed to obtain permission, unable to enter the game.";
        public override string RewardLabel => "REWARD";
        public override string PunishmentLabel => "PUNISHMENT";
        public override string PowerRewardTip => "Reward a point of energy!";
        public override string ClearBottomRowAllBlocksRewardTip => "Eliminate all the blocks in the bottom row!";
        public override string ClearSingleColorAllBlocksRewardTip => "Eliminate all blocks of any color from the board!";
        public override string GenerateNewRowAtBottomPunishmentTip => "A row of Blocks is generated at the bottom!";
        public override string GenerateRandomNormalBlockAtTopPunishmentTip => "Generate N blocks of random colors to fall!";
        public override string ToastInfoType => "INFO";
        public override string ToastWarningType => "WARNING";
        public override string ToastErrorType => "ERROR";
        public override string RoundCountLabel => "Rounds";
        public override string GetRewardTip => "Get Reward Successfully!";
        public override string AdLoadFail => "Ad load failed!";
    }

}