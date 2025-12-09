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
    }
}