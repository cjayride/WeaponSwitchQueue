using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Cjayride.WeaponSwitchQueue
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "cjayride.weaponswitchqueue";
        public const string PluginName = "WeaponSwitchQueue";
        public const string PluginVersion = "1.0.4";

        internal static ConfigEntry<bool> QueueWeaponChanges;
        internal static ConfigEntry<bool> QueueHideWeapons;
        internal static ConfigEntry<bool> HideWithoutAnimation;
        internal static ConfigEntry<bool> InstantEquip;
        internal static ConfigEntry<float> QueueSeconds;
        internal static ConfigEntry<bool> DebugLogging;

        static Plugin _instance;

        private void Awake()
        {
            _instance = this;

            QueueWeaponChanges = Config.Bind("General", "QueueWeaponChanges", true,
                "Hotbar weapon presses made during an attack animation are applied when the swing finishes.");
            QueueHideWeapons = Config.Bind("General", "QueueHideWeapons", true,
                "Pressing the sheath key during an attack hides your weapons when the swing finishes.");
            HideWithoutAnimation = Config.Bind("General", "HideWithoutAnimation", true,
                "Skip the sheath animation when a queued hide runs.");
            InstantEquip = Config.Bind("General", "InstantEquip", false,
                "Skip the equip time of the queued weapon. Off keeps the normal equip delay.");
            QueueSeconds = Config.Bind("General", "QueueSeconds", 3f,
                new ConfigDescription("Forget a queued press if the attack has not ended within this many seconds.",
                    new AcceptableValueRange<float>(0.5f, 10f)));
            DebugLogging = Config.Bind("General", "DebugLogging", false,
                "Log every queued and applied switch to the BepInEx console.");

            new Harmony(PluginGuid).PatchAll();
            Logger.LogInfo("WeaponSwitchQueue " + PluginVersion + " loaded. Client only.");
        }

        private void Update()
        {
            Player player = Player.m_localPlayer;
            if (player == null)
            {
                WeaponQueue.Clear();
                return;
            }

            if (player.m_nview == null || !player.m_nview.IsValid() || !player.m_nview.IsOwner())
            {
                return;
            }

            if (QueueHideWeapons.Value && player.InAttack() && player.TakeInput()
                && (ZInput.GetButtonDown("Hide") || ZInput.GetButtonDown("JoyHide"))
                && (player.GetRightItem() != null || player.GetLeftItem() != null))
            {
                WeaponQueue.QueueHide();
            }

            WeaponQueue.Tick(player, Time.deltaTime);
        }

        internal static void Debug(string message)
        {
            if (_instance != null && DebugLogging != null && DebugLogging.Value)
            {
                _instance.Logger.LogInfo(message);
            }
        }
    }
}
