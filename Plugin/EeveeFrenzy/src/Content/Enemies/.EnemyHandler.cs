﻿using EeveeFrenzy.src.Util;
using EeveeFrenzy.src.Util.AssetLoading;

namespace EeveeFrenzy.src.Content.Enemies;

public class EnemyHandler : ContentHandler<EnemyHandler>
{
    public class PokemonEnemyAssets(string bundleName) : AssetBundleLoader<PokemonEnemyAssets>(bundleName)
    {
        [LoadFromBundle("FlareonObj.asset")]
        public EnemyType FlareonEnemyType { get; private set; } = null!;

        [LoadFromBundle("MechaSylveonObj.asset")]
        public EnemyType MechaSylveonEnemyType { get; private set; } = null!;

        [LoadFromBundle("GlaceonObj.asset")]
        public EnemyType GlaceonEnemyType { get; private set; } = null!;

        [LoadFromBundle("VaporeonObj.asset")]
        public EnemyType VaporeonEnemyType { get; private set; } = null!;

        [LoadFromBundle("JolteonObj.asset")]
        public EnemyType JolteonEnemyType { get; private set; } = null!;

        [LoadFromBundle("UmbreonObj.asset")]
        public EnemyType UmbreonEnemyType { get; private set; } = null!;

        [LoadFromBundle("EspeonObj.asset")]
        public EnemyType EspeonEnemyType { get; private set; } = null!;

        [LoadFromBundle("LeafeonObj.asset")]
        public EnemyType LeafeonEnemyType { get; private set; } = null!;

        [LoadFromBundle("ChildEeveeObj.asset")]
        public Item ChildEeveeItem { get; private set; } = null!;
    }

    public PokemonEnemyAssets PokemonEnemies { get; private set; } = null!;

    public EnemyHandler()
    {
        if (true)
        {
            PokemonEnemies = new PokemonEnemyAssets("eeveelutionassets");
            RegisterScrapWithConfig("", PokemonEnemies.ChildEeveeItem, -1, -1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigUmbreonSpawnWeight.Value, PokemonEnemies.UmbreonEnemyType, null, null, 2, 1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigVaporeonSpawnWeight.Value, PokemonEnemies.VaporeonEnemyType, null, null, 2, 1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigJolteonSpawnWeight.Value, PokemonEnemies.JolteonEnemyType, null, null, 2, 1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigEspeonSpawnWeight.Value, PokemonEnemies.EspeonEnemyType, null, null, 2, 1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigGlaceonSpawnWeight.Value, PokemonEnemies.GlaceonEnemyType, null, null, 2, 1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigMechaSylveonSpawnWeight.Value, PokemonEnemies.MechaSylveonEnemyType, null, null, 2, 1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigFlareonSpawnWeight.Value, PokemonEnemies.FlareonEnemyType, null, null, 2, 1);
            RegisterEnemyWithConfig(Plugin.ModConfig.ConfigLeafeonSpawnWeight.Value, PokemonEnemies.LeafeonEnemyType, null, null, 2, 1);
        }
    }
}