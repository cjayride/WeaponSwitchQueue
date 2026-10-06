namespace Cjayride.WeaponSwitchQueue
{
    internal static class WeaponQueue
    {
        static ItemDrop.ItemData _pending;
        static bool _wantEquipped;
        static float _age;

        static bool _hide;
        static float _hideAge;

        internal static void Enqueue(Player player, ItemDrop.ItemData item)
        {
            _pending = item;
            _wantEquipped = !player.IsItemEquiped(item);
            _age = 0f;
            Plugin.Debug((_wantEquipped ? "Queued equip: " : "Queued unequip: ") + item.m_shared.m_name);
        }

        internal static void QueueHide()
        {
            _hide = true;
            _hideAge = 0f;
            Plugin.Debug("Queued hide");
        }

        internal static void Clear()
        {
            _pending = null;
            _hide = false;
        }

        internal static void Tick(Player player, float dt)
        {
            float window = Plugin.QueueSeconds.Value;

            if (_pending != null)
            {
                _age += dt;
                if (_age > window)
                {
                    Plugin.Debug("Dropped queued switch (timed out)");
                    _pending = null;
                }
                else if (!player.InAttack() && !player.InDodge())
                {
                    ItemDrop.ItemData item = _pending;
                    bool wantEquipped = _wantEquipped;
                    _pending = null;
                    Apply(player, item, wantEquipped);
                }
            }

            if (!_hide)
            {
                return;
            }

            _hideAge += dt;
            if (_hideAge > window)
            {
                _hide = false;
                return;
            }

            if (player.InAttack() || player.InDodge())
            {
                return;
            }

            _hide = false;
            player.HideHandItems(false, !Plugin.HideWithoutAnimation.Value);
            Plugin.Debug("Applied queued hide");
        }

        static void Apply(Player player, ItemDrop.ItemData item, bool wantEquipped)
        {
            if (item == null || player.m_inventory == null || !player.m_inventory.ContainsItem(item))
            {
                Plugin.Debug("Dropped queued switch (item gone)");
                return;
            }

            if (player.IsItemEquiped(item) == wantEquipped)
            {
                Plugin.Debug("Queued switch already satisfied");
                return;
            }

            float duration = item.m_shared.m_equipDuration;
            if (Plugin.InstantEquip.Value)
            {
                item.m_shared.m_equipDuration = 0f;
            }

            player.ToggleEquipped(item);
            item.m_shared.m_equipDuration = duration;
            Plugin.Debug("Applied queued switch: " + item.m_shared.m_name);
        }
    }
}
