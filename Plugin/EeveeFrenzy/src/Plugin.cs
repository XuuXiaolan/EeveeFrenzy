using System;
using System.Reflection;
using UnityEngine;
using BepInEx;
using BepInEx.Logging;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using EeveeFrenzy.src.Util.AssetLoading;
using EeveeFrenzy.src.Util.Extensions;
using EeveeFrenzy.src.Patches;
using EeveeFrenzy.src.Util;
using Unity.Netcode;

namespace EeveeFrenzy.src;
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(LethalLib.Plugin.ModGUID, BepInDependency.DependencyFlags.HardDependency)] 
[BepInDependency(LethalLevelLoader.Plugin.ModGUID, BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BaseUnityPlugin {
    internal static new ManualLogSource Logger = null!;
    internal static readonly Harmony _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    internal static readonly Dictionary<string, AssetBundle> LoadedBundles = [];
    internal static readonly Dictionary<string, Item> samplePrefabs = [];
    public static EeveeFrenzyConfig ModConfig { get; private set; } = null!; // prevent from accidently overriding the config

    internal static MainAssets Assets { get; private set; } = null!;
    internal class MainAssets(string bundleName) : AssetBundleLoader<MainAssets>(bundleName)
    {
        [LoadFromBundle("EeveeFrenzyUtils.prefab")]
        public GameObject UtilsPrefab { get; private set; } = null!;
    }
    
    private void Awake()
    {
        Logger = base.Logger;
        ModConfig = new EeveeFrenzyConfig(this.Config); // Create the config with the file from here.
#if DEBUG
        ModConfig.ConfigEnableExtendedLogging.Value = true;
#endif

        PlayerControllerBPatch.Init();
        DoorLockPatch.Init();
        MineshaftElevatorControllerPatch.Init();

        _harmony.PatchAll(typeof(StartOfRoundPatch));
        // This should be ran before Network Prefabs are registered.
        
        Assets = new MainAssets("eeveefrenzyassets");
        InitializeNetworkBehaviours();
        
        Logger.LogInfo("Registering content.");

        RegisterContentHandlers(Assembly.GetExecutingAssembly());

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    public static void RegisterContentHandlers(Assembly assembly) {
        List<Type> contentHandlers = assembly.GetLoadableTypes().Where(x =>
            x.BaseType != null
            && x.BaseType.IsGenericType
            && x.BaseType.GetGenericTypeDefinition() == typeof(ContentHandler<>)
        ).ToList();
        
        foreach(Type type in contentHandlers)
        {
            type.GetConstructor([]).Invoke([]);
        }
    }
    
    private void OnDisable()
    {
        foreach (AssetBundle bundle in LoadedBundles.Values)
        {
            bundle.Unload(false);
        }
        Logger.LogDebug("Unloaded assetbundles.");
        LoadedBundles.Clear();
    }

    internal static void ExtendedLogging(object text)
    {
        if (ModConfig.ConfigEnableExtendedLogging.Value)
        {
            Logger.LogInfo(text);
        }
    }

    private void InitializeNetworkBehaviours()
    {
        var types = Assembly.GetExecutingAssembly().GetLoadableTypes();
        foreach (var type in types)
        {
            if (type.IsNested || !typeof(NetworkBehaviour).IsAssignableFrom(type))
            {
                continue; // we do not care about fixing it, if it is not a network behaviour
            }
            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                if (attributes.Length > 0)
                {
                    method.Invoke(null, null);
                }
            }
        }
    }
}