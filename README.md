# CS2 DamageMod
Modify hitgroups to be enabled/disabled.

## Requirements
A [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) & [MetaMod:Source](https://www.sourcemm.net/) installation on your server.

## Installation
1. Download from Releases/Artifacts or compile yourself.
2. Open root of CS2 installation on server.
3. Copy DamageMod folder from the zip into `game/csgo/addons/counterstrikesharp/plugins` on server.

## Configuration

Config allows you to enable/disable and change damage multiplier on individual hitgroups.
```json
    "HITGROUP_HEAD": {
      "Enabled": true,
      "DamageMultiplier": 0.25
    },
    "HITGROUP_CHEST": {
      "Enabled": false,
      "DamageMultiplier": 1
    },
```
This modifies allows damage to the head hitgroup & multipies all damage to the head hitgroup by 0.25 (divided by 4).\
It also disables the chest hitgroup, disabling all damage to the chest even though the multiplier is 1.
