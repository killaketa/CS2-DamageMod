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

public class HitgroupInfo
{
    [JsonPropertyName("Enabled")] public bool Enabled { get; set; } = true;
    [JsonPropertyName("DamageMultiplier")] public float DamageMultiplier { get; set; } = 1;
}

public class HitGroups
{
    [JsonPropertyName("HITGROUP_HEAD")] public HitgroupInfo HITGROUP_HEAD { get; set; } = new();
    [JsonPropertyName("HITGROUP_NECK")] public HitgroupInfo HITGROUP_NECK { get; set; } = new();
    [JsonPropertyName("HITGROUP_CHEST")] public HitgroupInfo HITGROUP_CHEST { get; set; } = new();
    [JsonPropertyName("HITGROUP_STOMACH")] public HitgroupInfo HITGROUP_STOMACH { get; set; } = new();
    [JsonPropertyName("HITGROUP_LEFTARM")] public HitgroupInfo HITGROUP_LEFTARM { get; set; } = new();
    [JsonPropertyName("HITGROUP_RIGHTARM")] public HitgroupInfo HITGROUP_RIGHTARM { get; set; } = new();
    [JsonPropertyName("HITGROUP_LEFTLEG")] public HitgroupInfo HITGROUP_LEFTLEG { get; set; } = new();
    [JsonPropertyName("HITGROUP_RIGHTLEG")] public HitgroupInfo HITGROUP_RIGHTLEG { get; set; } = new();
    [JsonPropertyName("HITGROUP_GEAR")] public HitgroupInfo HITGROUP_GEAR { get; set; } = new();
    [JsonPropertyName("HITGROUP_GENERIC")] public HitgroupInfo HITGROUP_GENERIC { get; set; } = new();
    [JsonPropertyName("HITGROUP_SPECIAL")] public HitgroupInfo HITGROUP_SPECIAL { get; set; } = new();
    [JsonPropertyName("HITGROUP_UNUSED")] public HitgroupInfo HITGROUP_UNUSED { get; set; } = new();
    [JsonPropertyName("HITGROUP_INVALID")] public HitgroupInfo HITGROUP_INVALID { get; set; } = new();
    [JsonPropertyName("HITGROUP_COUNT")] public HitgroupInfo HITGROUP_COUNT { get; set; } = new();
}

public class DamageModConfig : BasePluginConfig
{
    [JsonPropertyName("Enabled")] public bool Enabled { get; set; } = true;
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
                switch (damageInfo.GetHitGroup())
                {
                    case HitGroup_t.HITGROUP_CHEST:
                        if (Config.HitGroups.HITGROUP_CHEST.Enabled)       { damageInfo.Damage *= Config.HitGroups.HITGROUP_CHEST.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_COUNT:
                        if (Config.HitGroups.HITGROUP_COUNT.Enabled)       { damageInfo.Damage *= Config.HitGroups.HITGROUP_COUNT.DamageMultiplier;  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_GEAR:
                        if (Config.HitGroups.HITGROUP_GEAR.Enabled)        { damageInfo.Damage *= Config.HitGroups.HITGROUP_GEAR.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_GENERIC:
                        if (Config.HitGroups.HITGROUP_GENERIC.Enabled)     { damageInfo.Damage *= Config.HitGroups.HITGROUP_GENERIC.DamageMultiplier;  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_HEAD:
                        if (Config.HitGroups.HITGROUP_HEAD.Enabled)        { damageInfo.Damage *= Config.HitGroups.HITGROUP_HEAD.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_INVALID:
                        if (Config.HitGroups.HITGROUP_INVALID.Enabled)     { damageInfo.Damage *= Config.HitGroups.HITGROUP_INVALID.DamageMultiplier;  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_LEFTARM:
                        if (Config.HitGroups.HITGROUP_LEFTARM.Enabled)     { damageInfo.Damage *= Config.HitGroups.HITGROUP_LEFTARM.DamageMultiplier;  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_LEFTLEG:
                        if (Config.HitGroups.HITGROUP_LEFTLEG.Enabled)     { damageInfo.Damage *= Config.HitGroups.HITGROUP_LEFTLEG.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_NECK:
                        if (Config.HitGroups.HITGROUP_NECK.Enabled)        { damageInfo.Damage *= Config.HitGroups.HITGROUP_NECK.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_RIGHTARM:
                        if (Config.HitGroups.HITGROUP_RIGHTARM.Enabled)    { damageInfo.Damage *= Config.HitGroups.HITGROUP_RIGHTARM.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_RIGHTLEG:
                        if (Config.HitGroups.HITGROUP_RIGHTLEG.Enabled)    { damageInfo.Damage *= Config.HitGroups.HITGROUP_RIGHTLEG.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_SPECIAL:
                        if (Config.HitGroups.HITGROUP_SPECIAL.Enabled)     { damageInfo.Damage *= Config.HitGroups.HITGROUP_SPECIAL.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_STOMACH:
                        if (Config.HitGroups.HITGROUP_STOMACH.Enabled)     { damageInfo.Damage *= Config.HitGroups.HITGROUP_STOMACH.DamageMultiplier; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_UNUSED:
                        if (Config.HitGroups.HITGROUP_UNUSED.Enabled)      { damageInfo.Damage *= Config.HitGroups.HITGROUP_UNUSED.DamageMultiplier;  return HookResult.Changed; }
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