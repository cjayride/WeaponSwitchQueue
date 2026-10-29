# WeaponSwitchQueue — Valheim 1.0.21 release

## Goal
- Verify Release build against Valheim 1.0.21, update BepInExPack to 5.4.2360, changelog, Thunderstore `/dist` zip, git tag `v1.0.4`.

## Status
- Completed

## Changes
- Version 1.0.4: manifest BepInEx `denikson-BepInExPack_Valheim-5.4.2360`, changelog, README compatibility note.
- `dist/` staged for Thunderstore (DLL, manifest, README, CHANGELOG, icon, LICENSE); zip `cjayride-WeaponSwitchQueue-1.0.4.zip`.

## Build
- `dotnet build Cjayride.WeaponSwitchQueue.csproj -c Release` — succeeded (0 warnings).
- DLL: `D:\AI-Agent-Projects\cjaycraft\Cjayride.WeaponSwitchQueue\bin\Release\net481\Cjayride.WeaponSwitchQueue.dll`

## Next
- Commit, push, tag `v1.0.4` on GitHub.
