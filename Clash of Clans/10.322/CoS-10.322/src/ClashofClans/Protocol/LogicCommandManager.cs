using System;
using System.Collections.Generic;
using ClashofClans.Protocol.Commands.Client;

namespace ClashofClans.Protocol
{
    public class LogicCommandManager
    {
        public static Dictionary<int, Type> Commands;

        static LogicCommandManager()
        {
            Commands = new Dictionary<int, Type>
            {
                {500, typeof(LogicBuyBuildingCommand)},
                {501, typeof(LogicMoveBuildingCommand)},
                {502, typeof(LogicUpgradeBuildingCommand)},
                {504, typeof(LogicSpeedUpConstructionCommand)},
                {505, typeof(LogicCancelConstructionCommand)},
                {506, typeof(LogicCollectResourcesCommand)},
                {507, typeof(LogicClearObstacleCommand)},
                {508, typeof(LogicTrainUnitCommand)},
                {509, typeof(LogicCancelUnitProductionCommand)},
                {510, typeof(LogicBuyTrapCommand)},
                {512, typeof(LogicBuyDecoCommand)},
                {513, typeof(LogicSpeedUpTrainingCommand)},
                {516, typeof(LogicUpgradeUnitCommand)},
                {517, typeof(LogicSpeedUpUpgradeUnitCommand)},
                {519, typeof(LogicMissionProgressCommand)},
                {520, typeof(LogicUnlockBuildingCommand)},
                {521, typeof(LogicFreeWorkerCommand)},
                {523, typeof(LogicCollectAchievementCommand)},
                {524, typeof(LogicToggleAttackModeCommand)},
                {526, typeof(LogicBoostBuildingCommand)},
                {527, typeof(LogicUpgradeHeroCommand)},
                {528, typeof(LogicSpeedUpHeroUpgradeCommand)},
                {529, typeof(LogicToggleHeroSleepCommand)},
                {532, typeof(LogicNewShopItemsSeenCommand)},
                {533, typeof(LogicMoveMultipleBuildingsCommand)},
                {538, typeof(LogicLeagueNotificationsSeenCommand)},
                {539, typeof(LogicNewsSeenCommand)},
                {544, typeof(LogicEditModeShownCommand)},
                {549, typeof(LogicUpgradeMultipleBuildingsCommand)},
                {550, typeof(LogicRemoveUnitCommand)},
                {577, typeof(LogicSwapBuildingsCommand)},
                {589, typeof(LogicPlacePendingBuildingCommand)},
                {590, typeof(LogicBuyWallCommand)},
                {591, typeof(LogicSwitchVillageStateCommand)},
                {592, typeof(LogicTrainUnitVillage2Command)},
                {593, typeof(LogicSpeedUpTrainingVillage2Command)},
                {597, typeof(LogicEventsSeenCommand)},
                {601, typeof(LogicMatchmakingVillage2Command)},
                {615, typeof(LogicBuyDailyDealItemCommand)},
                {623, typeof(LogicUpgradeWeaponCommand)},
                {624, typeof(LogicToggleClanCastleSleepCommand)},
                {640, typeof(LogicAttackNpcCommand)},
                {654, typeof(LogicTriggerHeroAbilityOnDeathCommand)},
                {688, typeof(LogicMoveBuildingCommand)},
                {700, typeof(LogicPlaceAttackerCommand)},
                {703, typeof(LogicEndCombatCommand)},
                {706, typeof(LogicTriggerHeroAbility)},
                {705, typeof(LogicPlaceHeroCommand)},
                {712, typeof(LogicTriggerUnitAbility)},
                {715, typeof(LogicSetHeroModeBattleCommand)},
                {772, typeof(LogicCastSpellCommand)},
                {788, typeof(LogicPlaceHeroCommand)},
                {800, typeof(LogicMatchmakingCommand)},
                {802, typeof(LogicAcceptChatRulesCommand)}
            };
        }
    }
}