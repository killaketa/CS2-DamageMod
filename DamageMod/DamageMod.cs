using System.Numerics;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Config;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

namespace DamageMod;

public class HitGroups
{
    [JsonPropertyName("HITGROUP_HEAD")] public bool HITGROUP_HEAD { get; set; } = true;
    [JsonPropertyName("HITGROUP_NECK")] public bool HITGROUP_NECK { get; set; } = true;
    [JsonPropertyName("HITGROUP_CHEST")] public bool HITGROUP_CHEST { get; set; } = true;
    [JsonPropertyName("HITGROUP_STOMACH")] public bool HITGROUP_STOMACH { get; set; } = true;
    [JsonPropertyName("HITGROUP_LEFTARM")] public bool HITGROUP_LEFTARM { get; set; } = true;
    [JsonPropertyName("HITGROUP_RIGHTARM")] public bool HITGROUP_RIGHTARM { get; set; } = true;
    [JsonPropertyName("HITGROUP_LEFTLEG")] public bool HITGROUP_LEFTLEG { get; set; } = true;
    [JsonPropertyName("HITGROUP_RIGHTLEG")] public bool HITGROUP_RIGHTLEG { get; set; } = true;
    [JsonPropertyName("HITGROUP_GEAR")] public bool HITGROUP_GEAR { get; set; } = true;
    [JsonPropertyName("HITGROUP_GENERIC")] public bool HITGROUP_GENERIC { get; set; } = true;
    [JsonPropertyName("HITGROUP_SPECIAL")] public bool HITGROUP_SPECIAL { get; set; } = true;
    [JsonPropertyName("HITGROUP_UNUSED")] public bool HITGROUP_UNUSED { get; set; } = true;
    [JsonPropertyName("HITGROUP_INVALID")] public bool HITGROUP_INVALID { get; set; } = true;
    [JsonPropertyName("HITGROUP_COUNT")] public bool HITGROUP_COUNT { get; set; } = true;
}

public class DamageModConfig : BasePluginConfig
{
    [JsonPropertyName("Enabled")] public bool Enabled { get; set; } = true;
    [JsonPropertyName("DamageMultiplier")] public float DamageMultiplier { get; set; } = 1;
    [JsonPropertyName("HitGroups")] public HitGroups HitGroups { get; set; } = new();
}

public class CDamageMod : BasePlugin, IPluginConfig<DamageModConfig>
{
    public override string ModuleName => "DamageMod";

    public override string ModuleVersion => "1.0";

    public override string ModuleAuthor => "keta";

    public override string ModuleDescription => "Modifies all damage interactions with the player.";

    public DamageModConfig Config { get; set; }

    public void OnConfigParsed(DamageModConfig config)
    {
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        Console.WriteLine("Loaded DamageMod");

        VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(h =>
        {
            var damageInfo = h.GetParam<CTakeDamageInfo>(1);

            if (Config.Enabled)
            {
                damageInfo.Damage *= Config.DamageMultiplier;

                switch (damageInfo.GetHitGroup())
                {
                    case HitGroup_t.HITGROUP_CHEST:
                        if (Config.HitGroups.HITGROUP_CHEST) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_COUNT:
                        if (Config.HitGroups.HITGROUP_COUNT) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_GEAR:
                        if (Config.HitGroups.HITGROUP_GEAR) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_GENERIC:
                        if (Config.HitGroups.HITGROUP_GENERIC) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_HEAD:
                        if (Config.HitGroups.HITGROUP_HEAD) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_INVALID:
                        if (Config.HitGroups.HITGROUP_INVALID) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_LEFTARM:
                        if (Config.HitGroups.HITGROUP_LEFTARM) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_LEFTLEG:
                        if (Config.HitGroups.HITGROUP_LEFTLEG) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_NECK:
                        if (Config.HitGroups.HITGROUP_NECK) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_RIGHTARM:
                        if (Config.HitGroups.HITGROUP_RIGHTARM) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_RIGHTLEG:
                        if (Config.HitGroups.HITGROUP_RIGHTLEG) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_SPECIAL:
                        if (Config.HitGroups.HITGROUP_SPECIAL) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_STOMACH:
                        if (Config.HitGroups.HITGROUP_STOMACH) { return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_UNUSED:
                        if (Config.HitGroups.HITGROUP_UNUSED) { return HookResult.Changed; }
                        break;
                }

                damageInfo.Damage = 0;
                return HookResult.Changed;
            }

            return HookResult.Continue;
        }, HookMode.Pre);

        RegisterEventHandler<EventBulletFlightResolution>((evt, info) =>
        {
            return HookResult.Continue;
        });
    }
}