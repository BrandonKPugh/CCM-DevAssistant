using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx;
using BepInEx.Logging;
using CCM_DevAssistant.Patches;
using HarmonyLib;

namespace CCM_DevAssistant
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class DevAssistantMain : BaseUnityPlugin
    {
        public const string modGUID = "CptnsCompatibleMods.DevAssistant";
        public const string modName = "CCM-DevAssistant";
        public const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static DevAssistantMain Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Loading " + modName);

            harmony.PatchAll(typeof(DevAssistantMain));
            harmony.PatchAll(typeof(StartMatchLeverPatch));

            mls.LogInfo("Finished loading " + modName);
        }
    }
}
