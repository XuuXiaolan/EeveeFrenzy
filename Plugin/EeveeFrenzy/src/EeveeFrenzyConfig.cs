using System.Collections.Generic;
using System.Reflection;
using BepInEx.Configuration;

namespace EeveeFrenzy.src;
public class EeveeFrenzyConfig
{
    #region Misc
    public ConfigEntry<bool> ConfigEnableExtendedLogging { get; private set; }
    public ConfigEntry<float> ConfigEverythingVolumeMultiplier { get; private set; }

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
        ConfigEverythingVolumeMultiplier = configFile.Bind("Eevee Frenzy",
                                            "Everything Volume Multiplier",
                                            0.5f,
                                            "Volume multiplier for Eevee Frenzy.");
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