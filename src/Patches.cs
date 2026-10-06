using HarmonyLib;

namespace Cjayride.WeaponSwitchQueue
{
    /// <summary>
    /// Player.ToggleEquipped returns true and does nothing while InAttack() is true, so the hotbar
    /// press is silently dropped. Remember it here and apply it once the swing ends.
    /// </summary>
    [HarmonyPatch(typeof(Player), "ToggleEquipped", new[] { typeof(ItemDrop.ItemData) })]
    internal static class PlayerToggleEquippedPatch
    {
        static void Postfix(Player __instance, bool __result, ItemDrop.ItemData item)
        {
            if (!Plugin.QueueWeaponChanges.Value || __instance != Player.m_localPlayer)
            {
                return;
            }

            if (!__result || item == null || !item.IsEquipable())
            {
                return;
            }

            if (!__instance.InAttack())
            {
                return;
            }

            WeaponQueue.Enqueue(__instance, item);
        }
    }
}
