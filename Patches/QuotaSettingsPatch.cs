using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine;

namespace CCM_DevAssistant.Patches
{
    [HarmonyPatch(typeof(StartMatchLever))]
    internal class StartMatchLeverPatch
    {
        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource(CCM_DevAssistant.DevAssistantMain.modGUID);

        private static int creditsToStartWith = 10000;

        [HarmonyPatch("StartGame")]
        [HarmonyPostfix]
        static void InfiniteStartingCredits(ref StartOfRound ___playersManager)
        {
            int days = ___playersManager.gameStats.daysSpent;

            //mls.LogInfo((object)$"Days survived: {days}");
            if (days > 0)
            {
                //mls.LogInfo((object)$"Days is greater than 0, skipping");
                return;
            }
            mls.LogInfo((object)$"Setting starting credits to: {creditsToStartWith}");
            HUDManager hudManager = (HUDManager)UnityEngine.Object.FindObjectOfType(typeof(HUDManager));
            Terminal terminal = (Terminal)UnityEngine.Object.FindObjectOfType(typeof(Terminal));
            terminal.SyncGroupCreditsServerRpc(creditsToStartWith, terminal.numberOfItemsInDropship);
            mls.LogInfo((object)$"New credits value: {terminal.groupCredits}");
            //hudManager.AddTextToChatOnServer($"Set credits: {terminal.groupCredits}", -1);
        }
    }
}
