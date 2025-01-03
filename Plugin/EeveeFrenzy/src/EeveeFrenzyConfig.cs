using System.Collections.Generic;
using System.Reflection;
using BepInEx.Configuration;

namespace EeveeFrenzy.src;
public class EeveeFrenzyConfig
{
    #region Misc
    public ConfigEntry<bool> ConfigEnableExtendedLogging { get; private set; }
    public ConfigEntry<string> ConfigUmbreonSpawnWeight { get; private set; }
    public ConfigEntry<string> ConfigEspeonSpawnWeight { get; private set; }
    public ConfigEntry<string> ConfigJolteonSpawnWeight { get; private set; }
    public ConfigEntry<string> ConfigLeafeonSpawnWeight { get; private set; }
    public ConfigEntry<string> ConfigMechaSylveonSpawnWeight { get; private set; }
    public ConfigEntry<string> ConfigGlaceonSpawnWeight { get; private set; }
    public ConfigEntry<string> ConfigVaporeonSpawnWeight { get; private set; }
    public ConfigEntry<string> ConfigFlareonSpawnWeight { get; private set; }

    #endregion 
    public EeveeFrenzyConfig(ConfigFile configFile)
    {
        configFile.SaveOnConfigSet = false;

        #region Debug
        ConfigEnableExtendedLogging = configFile.Bind("Debug Options",
                                            "Debug Mode | Enable Extended Logging",
                                            false,
                                            "Whether extended logging is enabled.");
        #endregion
        #region Eevee Frenzy
        ConfigUmbreonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Umbreon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Umbreon.");
        ConfigEspeonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Espeon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Espeon.");
        ConfigJolteonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Jolteon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Jolteon.");
        ConfigLeafeonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Leafeon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Leafeon.");
        ConfigMechaSylveonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Sylveon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Sylveon.");
        ConfigGlaceonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Glaceon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Glaceon.");
        ConfigVaporeonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Vaporeon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Vaporeon.");
        ConfigFlareonSpawnWeight = configFile.Bind("Eevee Frenzy",
                                            "Flareon Spawn Weight",
                                            "Vanilla:10,Custom:10",
                                            "Spawn weight of Flareon.");
        #endregion
        configFile.SaveOnConfigSet = true;
        ClearUnusedEntries(configFile);
    }

    private void ClearUnusedEntries(ConfigFile configFile)
    {
        // Normally, old unused config entries don't get removed, so we do it with this piece of code. Credit to Kittenji.
        PropertyInfo orphanedEntriesProp = configFile.GetType().GetProperty("OrphanedEntries", BindingFlags.NonPublic | BindingFlags.Instance);
        var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp.GetValue(configFile, null);
        orphanedEntries.Clear(); // Clear orphaned entries (Unbinded/Abandoned entries)
        configFile.Save(); // Save the config file to save these changes
    }
}