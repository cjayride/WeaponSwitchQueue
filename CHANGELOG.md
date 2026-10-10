# 1.0.3

- Fix: a queued weapon switch still applies while you are sprinting. Valheim clears its equip bar on every sprint tick, so the switch was wiped before it finished. Standing still is unchanged.

# 1.0.2

- Fix: the queue is drained from the plugin's own update loop. 1.0.0 and 1.0.1 attached every patch to `Player.ToggleEquipped`, so a queued switch was never applied.
- Queue remembers whether the press meant equip or unequip, and drops it if the item leaves your inventory or the attack never ends (`QueueSeconds`).
- New: `InstantEquip` (off) and `DebugLogging` (off).

# 1.0.1

- Fix: patch Player.ToggleEquipped (not Humanoid). Hotbar switches during attacks are queued again.

# 1.0.0

- Queue hotbar weapon changes during attacks; apply on next FixedUpdate after the swing ends.
- Optional queued hide (sheath) with instant or animated hide.
