﻿using Dusk;

namespace EeveeFrenzy.src.Content.Enemies;

public class EnemyHandler : ContentHandler<EnemyHandler>
{
    public DefaultBundle? defaultBundle = null;

    public EnemyHandler(DuskMod mod) : base(mod)
    {
        RegisterContent("eeveelutionassets", out defaultBundle);
    }
}