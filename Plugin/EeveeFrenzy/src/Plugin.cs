using System.Reflection;
using UnityEngine;
using BepInEx;
using BepInEx.Logging;
using EeveeFrenzy.src.Patches;
using Dusk;
using Dawn.Utils;
using HarmonyLib;

namespace EeveeFrenzy.src;
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(Dusk.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)] 
public class Plugin : BaseUnityPlugin {
    internal static new ManualLogSource Logger = null!;
    internal static readonly Harmony _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    public static EeveeFrenzyConfig ModConfig { get; private set; } = null!;
    public static DuskMod Mod { get; private set; } = null!;
    internal static MainAssets Assets { get; private set; } = null!;
    internal class MainAssets(AssetBundle bundle) : AssetBundleLoader<MainAssets>(bundle)
    {
        [LoadFromBundle("EeveeFrenzyUtils.prefab")]
        public GameObject UtilsPrefab { get; private set; } = null!;
    }

    private void Awake()
    {
        Logger = base.Logger;
        ModConfig = new EeveeFrenzyConfig(this.Config);
#if DEBUG
        ModConfig.ConfigEnableExtendedLogging.Value = true;
#endif

        DoorLockPatch.Init();
        _harmony.PatchAll(typeof(StartOfRoundPatch));

        AssetBundle mainBundle = AssetBundleUtils.LoadBundle(Assembly.GetExecutingAssembly(), "eeveefrenzyassets");
        Assets = new MainAssets(mainBundle);
        Mod = DuskMod.RegisterMod(this, mainBundle);
        Mod.Logger = Logger;

        // Register Content
        Mod.RegisterContentHandlers();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void ExtendedLogging(object text)
    {
        if (ModConfig.ConfigEnableExtendedLogging.Value)
        {
            Logger.LogInfo(text);
        }
    }
}