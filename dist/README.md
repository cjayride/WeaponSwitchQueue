# Weapon Switch Queue

Queue weapon switches during attack animations in Valheim. Press a hotbar slot while swinging, and your weapon will equip automatically when the attack finishes—even while sprinting.

## Features

- **Queued Weapon Switching** — Switch weapons mid-swing without interrupting your attack.
- **Sprint Support** — Queued weapon switches still apply while sprinting.
- **Optional Queued Sheathing** — Queue a weapon hide/sheath action using the same method.
- **Instant or Animated Sheathing** — Choose how queued hide actions are performed.
- **Client-Side Mod** — No server installation required.
- **Inventory Validation** — Cancels queued switches if the item leaves your inventory.
- **Queue Timeout** — Prevents stale requests from remaining queued indefinitely.

## Configuration

Config file:

`BepInEx/config/cjayride.weaponswitchqueue.cfg`

Available options include:

- `InstantEquip` — Instantly equips queued weapons. Default: `false`.
- `DebugLogging` — Enables debug logging. Default: `false`.
- `QueueSeconds` — Sets how long a queued action can wait before expiring.

## Compatibility

- Verified against Valheim 1.0.21. BepInEx dependency: `denikson-BepInExPack_Valheim-5.4.2360`.
- This mod replaces **Queue Me Maybe** for cjayride packs (same idea, maintained GUID).