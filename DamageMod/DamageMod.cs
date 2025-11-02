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
    [JsonPropertyName("HITGROUP_HEAD")] public List<object> HITGROUP_HEAD { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_NECK")] public List<object> HITGROUP_NECK { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_CHEST")] public List<object> HITGROUP_CHEST { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_STOMACH")] public List<object> HITGROUP_STOMACH { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_LEFTARM")] public List<object> HITGROUP_LEFTARM { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_RIGHTARM")] public List<object> HITGROUP_RIGHTARM { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_LEFTLEG")] public List<object> HITGROUP_LEFTLEG { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_RIGHTLEG")] public List<object> HITGROUP_RIGHTLEG { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_GEAR")] public List<object> HITGROUP_GEAR { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_GENERIC")] public List<object> HITGROUP_GENERIC { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_SPECIAL")] public List<object> HITGROUP_SPECIAL { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_UNUSED")] public List<object> HITGROUP_UNUSED { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_INVALID")] public List<object> HITGROUP_INVALID { get; set; } = [true, 1];
    [JsonPropertyName("HITGROUP_COUNT")] public List<object> HITGROUP_COUNT { get; set; } = [true, 1];
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
                        if ((bool)Config.HitGroups.HITGROUP_CHEST[0])       { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_CHEST[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_COUNT:
                        if ((bool)Config.HitGroups.HITGROUP_COUNT[0])       { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_COUNT[1];  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_GEAR:
                        if ((bool)Config.HitGroups.HITGROUP_GEAR[0])        { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_GEAR[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_GENERIC:
                        if ((bool)Config.HitGroups.HITGROUP_GENERIC[0])     { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_GENERIC[1];  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_HEAD:
                        if ((bool)Config.HitGroups.HITGROUP_HEAD[0])        { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_HEAD[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_INVALID:
                        if ((bool)Config.HitGroups.HITGROUP_INVALID[0])     { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_INVALID[1];  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_LEFTARM:
                        if ((bool)Config.HitGroups.HITGROUP_LEFTARM[0])     { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_LEFTARM[1];  return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_LEFTLEG:
                        if ((bool)Config.HitGroups.HITGROUP_LEFTLEG[0])     { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_LEFTLEG[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_NECK:
                        if ((bool)Config.HitGroups.HITGROUP_NECK[0])        { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_NECK[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_RIGHTARM:
                        if ((bool)Config.HitGroups.HITGROUP_RIGHTARM[0])    { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_RIGHTARM[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_RIGHTLEG:
                        if ((bool)Config.HitGroups.HITGROUP_RIGHTLEG[0])    { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_RIGHTLEG[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_SPECIAL:
                        if ((bool)Config.HitGroups.HITGROUP_SPECIAL[0])     { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_SPECIAL[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_STOMACH:
                        if ((bool)Config.HitGroups.HITGROUP_STOMACH[0])     { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_STOMACH[1]; return HookResult.Changed; }
                        break;
                    case HitGroup_t.HITGROUP_UNUSED:
                        if ((bool)Config.HitGroups.HITGROUP_UNUSED[0])      { damageInfo.Damage *= (float)Config.HitGroups.HITGROUP_UNUSED[1];  return HookResult.Changed; }
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