# CS2 DamageMod
Modify hitgroups to be enabled/disabled.

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
